using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Login;

namespace TCRS.Business
{
    public class UserManager : IUserManager
    {
        public async Task<UserWithToken> UserSignIn(UserLoginCredentials userLoginCredentials)
        {
            return await Task.FromResult(new UserWithToken());
        }

    }
}
