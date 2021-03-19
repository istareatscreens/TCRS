using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Shared.Contracts
{
    public interface IPersistenceService
    {
        Task<UserTokens> AuthenticateAndGetUserAsync(UserLoginCredentials userLoginCredentials);
         Task<IEnumerable<T>> GetAsync<T>(List<KeyValuePair<string, string>> parametersList);
    }

}
