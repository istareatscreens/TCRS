using System.Collections.Generic;
using System.Threading.Tasks;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Shared.Contracts
{
    public interface IPersistenceService
    {
        //Put (Update)
        Task PutAsync<T>(T data);
        //Get
        Task<UserTokens> AuthenticateAndGetUserAsync(UserLoginCredentials userLoginCredentials);
        Task<IEnumerable<T>> GetAsync<T>(List<KeyValuePair<string, string>> parametersList);
        Task<IEnumerable<T>> GetAsync<T>();

        //Post 
        Task PostAsync<T>(T data);
        Task PostAsync<T>(T data, List<KeyValuePair<string, string>> parametersList);
        Task<IEnumerable<U>> PostAsync<T, U>(T data, List<KeyValuePair<string, string>> parametersList);
        Task<IEnumerable<U>> PostAsync<T, U>(T data);

    }

}
