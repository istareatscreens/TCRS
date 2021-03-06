using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Business
{
    public class UserManager : IUserManager
    {
        private readonly IPersistenceService _api;
        public UserManager(IPersistenceService api)
        {
            _api = api;
        }

        public async Task<UserTokens> UserSignIn(UserLoginCredentials userLoginCredentials)
        {
            return await _api.AuthenticateAndGetUserAsync(userLoginCredentials);
        }

    }
}
