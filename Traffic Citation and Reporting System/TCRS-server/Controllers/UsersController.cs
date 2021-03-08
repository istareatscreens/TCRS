using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
            var result = (await _db.LoadData<Person, DynamicParameters>("SELECT * FROM person WHERE email = @Email AND password = @Password",
                new DynamicParameters(new Person { email = user.email, password = user.password }), _databaseContext.Server)).ToList<Person>();
            user = (result.Count==1)?
                new Person{
                person_id = result[0].person_id,
                email = result[0].email,
                first_name = result[0].first_name,
                last_name = result[0].last_name
                } : null;

            UserWithToken userWithToken = null;

            if (user == null)
            {
                return NotFound("User Not Found");
            }
            else if (user != null)
            {

                var roles = await _db.GetUserRoles(user, _databaseContext.Server);
                userWithToken = new UserWithToken(user, new List<string> { Role.Admin });
                userWithToken.RefreshToken = GenerateRefreshToken().token;
                //save refresh token in database

            }

            //Generate JWT token
            userWithToken.AccessToken = GenerateJWT(user.person_id);
            return userWithToken;
        }


        //Refresh access
        [HttpPost("refreshtoken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            //Get user of expired access token
            Person user = GetUserFromAccessToken(refreshRequest.AccessToken);

            //Check if user actually matches refresh token that was sent by the client and that its not expired
            if (user != null && ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                //Need to access database and check roles! generate new token for user
                UserWithToken userWithToken = new UserWithToken(user, new List<string> { Role.Admin });
                userWithToken.AccessToken = GenerateJWT(user.person_id);

                //Pass back to client
                return userWithToken;

            }
            return null;
        }

        private Person GetUserFromAccessToken(string accessToken)
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
                var user_id = prinicpal.FindFirst(ClaimTypes.Name)?.Value;
                //Get user from database INSERT userbase input and convert to INT32
                //where User.UserID = User

                //return user;

            }

            return null;
        }

        private bool ValidateRefreshToken(Person user, string refreshToken)
        {

            //Check database if the refresh token exists in the database refershToken.token == refreshToken
            //Get refresh token and save it in refreshToken variable
            RefreshToken userRefreshToken = new RefreshToken();

            //check if refresh token is valid and for proper user
            if (userRefreshToken != null && userRefreshToken.person_id == user.person_id && userRefreshToken.expiry_date > DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }

        private string GenerateJWT(int person_id)
        {
            //Sign token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, person_id.ToString())
                }),
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
