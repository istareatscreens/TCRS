using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Business
{
    public class UserManager : IUserManager
    {
        private readonly IPersistanceService _api; 
        public UserManager(IPersistanceService api)
        {
            _api = api;
        }

        public async Task<UserWithToken> UserSignIn(UserLoginCredentials userLoginCredentials)
        {
            return await _api.AuthenticateAndGetUserAsync(userLoginCredentials);
            /*
            return await Task.FromResult(new UserWithToken
            {
                email=userLoginCredentials.Email
            });
            */
        }

    }
}
