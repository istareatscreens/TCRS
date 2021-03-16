namespace TCRS.Shared.Objects.Login
{
    public class UserWithToken 
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int person_id { get;  set; }
        public string email { get;  set; }
        public string first_name { get;  set; }
        public string last_name { get; set; }
        public bool isClientAdmin  { get;  set; }
        public bool isHighway_Patrol_Officer  { get;  set; }
        public bool isMunicipal_Officer  { get;  set; }
        public bool isSchool_Rep  { get;  set; }
        public bool isManager  { get;  set; }
    }
}
