using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace TCRS.Shared.Helper
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> GetClaimsFromJWT(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            //if jwt contains Bearer token then remove "Bearer " from start else don't
            var claimsList = (handler.ReadJwtToken((jwt.Substring(0, 7).Contains("Bearer")) ? jwt.Substring(7) : jwt)).Claims.ToList();

            //Get basic information defined in server
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, claimsList[0].Value),
                    new Claim(ClaimTypes.Email, claimsList[1].Value),
                    new Claim(ClaimTypes.GivenName, claimsList[2].Value),
                    new Claim(ClaimTypes.Surname, claimsList[3].Value),

            };

            //Get roles
            for (int i = 4; i < claimsList.Count(); i++)
            {
                if (claimsList[i].Type == "role")
                {
                    claims.Add(new Claim(ClaimTypes.Role, claimsList[i].Value));
                }
                else
                {
                    claims.Add(new Claim(claimsList[i].Type, claimsList[i].Value));
                }
            }

            return claims;
        }

    }
}
