using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.APIAccess
{
    public class APIAccessService : IPersistenceService
    {

        private readonly HttpClient _httpClient;

        public APIAccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(List<KeyValuePair<string, string>> parametersList)
        {
            //Add query parameters to url
            var queryParameters = "";
            if (parametersList != null && parametersList.Count != 0)
            {
                queryParameters = parametersList.Aggregate("?", (current, parameter)
                    => current + $"{parameter.Key}={parameter.Value}&");
                //remove trailing &
                queryParameters = queryParameters.Remove(queryParameters.Length - 1);
            }

            var requestUrl = RouteByType.GetEntityRouteAssignment[typeof(T)] + queryParameters;
            return await _httpClient.GetFromJsonAsync<IList<T>>(requestUrl);
        }

        public async Task<IEnumerable<T>> GetAsync<T>(){
            return await GetAsync<T>(new List<KeyValuePair<string, string>>());
       }

        public async Task<UserTokens> AuthenticateAndGetUserAsync(UserLoginCredentials userLoginCredentials)
        {
            //Post to login end point, get body content, read content as string, deserialize object to json object
             return JsonConvert.DeserializeObject<UserTokens>(
                await (await _httpClient.PostAsJsonAsync("/api/Users/login", userLoginCredentials))
                    .Content
                    .ReadAsStringAsync());
        }
    }
}
