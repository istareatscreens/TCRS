using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS_db.Model;
using TCRS_server.Users;

namespace TCRS_server.Controllers
{
    public class UserWithToken : Person
    {

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<string> roles { get; set; }
        public UserWithToken(Person user, List<string> roles)
        {
            this.person_id = user.person_id;
            this.email = user.email;
            this.first_name = user.first_name;
            this.last_name = user.last_name;
            this.roles = roles;
        }
    }
}
