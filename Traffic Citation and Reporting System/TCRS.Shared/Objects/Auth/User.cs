using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace TCRS.Shared.Objects.Auth
{
    public class User
    {

        public User() { }
        public User(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            //if jwt contains Bearer token then remove "Bearer " from start else don't
            var token = (handler.ReadJwtToken((jwt.Substring(0, 7).ToLower().Contains("bearer")) ? jwt.Substring(7) : jwt));
            foreach (var value in token.Claims.ToList())
            {
                switch (value.Type)
                {
                    case "unique_name":
                        person_id = Int32.Parse(value.Value);
                        break;
                    case "email":
                        email = value.Value;
                        break;
                    case "given_name":
                        first_name = value.Value;
                        break;
                    case "family_name":
                        last_name = value.Value;
                        break;
                    case "role":
                        //handle assignment of roles
                        AssignRole(value.Value);
                        break;
                }
            }
        }

        private void AssignRole(string role)
        {
            switch (role)
            {
                case Roles.Manager:
                    isManager = true;
                    break;
                case Roles.Admin:
                    isClientAdmin = true;
                    break;
                case Roles.MunicipalOfficer:
                    isMunicipal_Officer = true;
                    break;
                case Roles.HighwayPatrolOfficer:
                    isHighway_Patrol_Officer = true;
                    break;
                case Roles.SchoolRep:
                    isSchool_Rep = true;
                    break;
            }
        }

        public int person_id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool isClientAdmin { get; set; }
        public bool isHighway_Patrol_Officer { get; set; }
        public bool isMunicipal_Officer { get; set; }
        public bool isSchool_Rep { get; set; }
        public bool isManager { get; set; }



    }
}
