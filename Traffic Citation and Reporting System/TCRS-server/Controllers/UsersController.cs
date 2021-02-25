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
        public async Task<ActionResult<UserWithToken>> Login([FromBody] Person loginInfo)
        {
            //Compare email and password provided to database insert logic and determine role

            //create UserWithToken -> test code!
            Person person = new Person();
            person.person_id = 1;
            person.email = "F";
            person.first_name = "F";
            person.last_name = "F";
            UserWithToken userWithToken = new UserWithToken(person, new List<string> { Role.Admin });

            /*
            if (userWithToken == null)
            {
                return NotFound("User Not Found");
            }
            */

            //Sign token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, person.email)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.AccessToken = tokenHandler.WriteToken(token);

            return userWithToken;

        }
    }

}
