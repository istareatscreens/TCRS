using System.Data.Common;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TCRS_db;
using TCRS_db.Model;
using TCRS_server.Tokens;
using TCRS_server.Users;

namespace TCRS_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly JWTSettings _jwtsettings;
        private readonly IDataAccess _db;
        public UsersController(IOptions<DatabaseContext> databaseContext, IDataAccess db, IOptions<JWTSettings> jwtsettings)
        {
            _databaseContext = databaseContext.Value;
            _db = db;
            _jwtsettings = jwtsettings.Value;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] Person user)
        {
            //Compare email and password provided to database insert logic and determine role
            user = await _db.GetUser(user, _databaseContext.Server);

            UserWithToken userWithToken = null;

            //checks if user exists or active status is false
            if (user == null || !user.active)
            {
                return NotFound("User Not Found");
            }
            else if (user != null)
            {
                userWithToken = new UserWithToken(user);

                var refreshToken = GenerateRefreshToken();
                refreshToken.person_id = user.person_id;
                userWithToken.RefreshToken = refreshToken.token;
                //save refresh token in database
                _db.SaveRefreshToken(refreshToken, _databaseContext.Server);
            }

            //Generate JWT token
            userWithToken.AccessToken = GenerateJWT(user);
            return userWithToken;
        }


        //Refresh access
        [HttpPost("refreshtoken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            //Get user of expired access token
            Person user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            //Check if user actually matches refresh token that was sent by the client and that its not expired
            if (user != null && await ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                //Need to access database and check roles! generate new token for user
                var userWithToken = new UserWithToken(user);
                userWithToken.AccessToken = GenerateJWT(user);

                //Pass back to client
                return userWithToken;

            }
            return null;
        }

        private async Task<Person> GetUserFromAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken securityToken;

            var prinicpal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {

                //TODO: make this a single query or refactor JWT generation
                //Get user from access token
                var person_id = prinicpal.FindFirst(ClaimTypes.Name)?.Value;
                Person user = (await _db.LoadData<Person, DynamicParameters>("SELECT email, password FROM person WHERE person_id = @person_id", new DynamicParameters(new {person_id = Convert.ToInt32(person_id)}), _databaseContext.Server)).FirstOrDefault<Person>();
                return await _db.GetUser(user, _databaseContext.Server);
            }

            return null;
        }

        private async Task<bool> ValidateRefreshToken(Person user, string refreshToken)
        {

            //Check if the refresh token exists in the database by user_id and token
            //Get latest refresh token order by desc
            RefreshToken userRefreshToken = (await _db.LoadData<RefreshToken, RefreshToken>("SELECT * FROM refreshtoken WHERE token = @token AND person_id = @person_id ORDER BY token_id DESC", new RefreshToken { person_id = user.person_id, token = refreshToken }, _databaseContext.Server)).FirstOrDefault<RefreshToken>();

            //check if refresh token is valid and for proper user
            return (userRefreshToken != null && userRefreshToken.expiry_date > DateTime.UtcNow);
        }

        private Claim[] getUserClaims(Person user)
        {

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.person_id.ToString()),
                    new Claim(ClaimTypes.Email, user.email),
                    new Claim(ClaimTypes.GivenName, user.first_name),
                    new Claim(ClaimTypes.Surname, user.last_name),
            };

            if (user.Client_Admin != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.Admin));
            }
            else if (user.Municipal_Officer != null)
            {
                //role
                claims.Add(new Claim(ClaimTypes.Role, Roles.MunicipalOfficer));
            }
            else if (user.School_Rep != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.SchoolRep));
            }
            else if (user.Highway_Patrol_Officer != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.HighwayPatrolOfficer));
            }
            else
            {
                throw new Exception("Unable to assign user role");
            }

            if (user.Municipality != null || user.Police_Dept != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, Roles.Manager));
            }
            return claims.ToArray();
        }

        private string GenerateJWT(Person user)
        {
            //Sign token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(getUserClaims(user)),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();
            refreshToken.token = Guid.NewGuid().ToString(); //Generate GUID
            refreshToken.expiry_date = DateTime.UtcNow.AddHours(16); //Refresh token lasts 16 hours

            return refreshToken;
        }
    }

}
