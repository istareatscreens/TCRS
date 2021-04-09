using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Business
{
    public class UserService : IUserService
    {
        public User User { get; set; }

        public string GetFullName()
        {
            if (User == null)
            {
                return "";
            }
            return User.first_name + " " + User.last_name;
        }
    }
}
