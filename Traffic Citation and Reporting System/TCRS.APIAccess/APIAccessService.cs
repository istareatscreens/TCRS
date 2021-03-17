using System;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Login;

namespace TCRS.APIAccess
{
    public class APIAccessService: IPersistanceService
    {
        public Task<UserWithToken> AuthenticateAndGetUserAsync(UserLoginCredentials user, IPersistanceService api)
        {
            throw new NotImplementedException();
        }
    }
}
