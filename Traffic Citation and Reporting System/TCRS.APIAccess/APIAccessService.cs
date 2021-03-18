using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Login;

namespace TCRS.APIAccess
{
    public class APIAccessService: IPersistanceService
    {

        private readonly HttpClient _httpClient;

        public APIAccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserWithToken> AuthenticateAndGetUserAsync(UserLoginCredentials userLoginCredentials)
        {
            Console.WriteLine(await _httpClient.PostAsJsonAsync("/api/Users/login", userLoginCredentials));

            return new UserWithToken
            {
                email = "HELLO"
            };
        }
    }
}
