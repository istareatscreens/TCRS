using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace TCRS.Shared.Helper
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> GetClaimsFromJWT(string jwt)
        {

            var claims = new List<Claim>();
            var handler = new JwtSecurityTokenHandler();
            //if jwt contains Bearer token then remove "Bearer " from start else don't
            var claimsList = (handler.ReadJwtToken((jwt.Substring(0,7).Contains("Bearer"))?jwt.Substring(7):jwt)).Claims.ToList();
            foreach(var claim in claimsList)
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }
            return claims;
        }
    }
}
