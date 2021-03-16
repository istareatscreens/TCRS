using TCRS.Database.Model;

namespace TCRS.Server.Users
{
    public class UserWithToken 
    {

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int person_id { get; private set; }
        public string email { get; private set; }
        public string first_name { get; private set; }
        public string last_name { get; private set; }
        public Client_Admin Client_Admin { get; private set; }
        public Highway_Patrol_Officer Highway_Patrol_Officer { get; private set; }
        public Municipal_Officer Municipal_Officer { get; private set; }
        public School_Rep School_Rep { get; private set; }
        public Municipality Municipality { get; private set; }
        public Police_Dept Police_Dept { get; private set; }

        public UserWithToken(Person user)
        {
            this.person_id = user.person_id;
            this.email = user.email;
            this.first_name = user.first_name;
            this.last_name = user.last_name;
            this.Client_Admin = user.Client_Admin;
            this.Highway_Patrol_Officer = user.Highway_Patrol_Officer;
            this.Municipal_Officer = user.Municipal_Officer;
            this.School_Rep = user.School_Rep;
            this.Municipality = user.Municipality;
            this.Police_Dept = user.Police_Dept;

        }
    }
}
