using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

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
            var tokens = JsonConvert.DeserializeObject<UserTokens>(await (await _httpClient.PostAsJsonAsync("/api/Users/login", userLoginCredentials)).Content
                .ReadAsStringAsync());

            Console.WriteLine(tokens.AccessToken);

            return new UserWithToken
            {
                email = "HELLO"
            };
        }
    }
}
