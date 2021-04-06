using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.APIAccess
{
    public class APIAccessService : IPersistenceService
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _settings;

        public APIAccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        public async Task<IEnumerable<T>> GetAsync<T>(List<KeyValuePair<string, string>> parametersList)
        {
            var requestUrl = RouteByType.GetEntityRouteAssignment[typeof(T)] + stringifyParameter(parametersList);
            return await _httpClient.GetFromJsonAsync<IList<T>>(requestUrl);
        }

        private string stringifyParameter(List<KeyValuePair<string, string>> parametersList)
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
            return queryParameters;
        }

        public async Task<IEnumerable<T>> GetAsync<T>()
        {
            return await GetAsync<T>(new List<KeyValuePair<string, string>>());
        }

        public async Task<IEnumerable<U>> PostAsync<T, U>(T data)
        {
            return await PostAsync<T, U>(data, new List<KeyValuePair<string, string>>());
        }

        public async Task<IEnumerable<U>> PostAsync<T, U>(T data, List<KeyValuePair<string, string>> parametersList)
        {
            var requestUrl = RouteByType.PostEntityRouteAssignment[typeof(T)] + stringifyParameter(parametersList);
            var request = await _httpClient.PostAsJsonAsync(requestUrl, data);
            if (!request.IsSuccessStatusCode)
            {
                Console.WriteLine(request.Content);
            }

            return JsonConvert.DeserializeObject<IEnumerable<U>>(
               await (request)
                   .Content
                   .ReadAsStringAsync(), _settings);
        }

        public async Task PutAsync<T>(T data)
        {
            var requestUrl = RouteByType.PutEntityRouteAssignment[typeof(T)];
            await _httpClient.PutAsJsonAsync(requestUrl, data);
        }

        public async Task PostAsync<T>(T data)
        {
            await PostAsync<T>(data, new List<KeyValuePair<string, string>>());
        }

        public async Task PostAsync<T>(T data, List<KeyValuePair<string, string>> parametersList)
        {
            var requestUrl = RouteByType.PostEntityRouteAssignment[typeof(T)] + stringifyParameter(parametersList);
            await _httpClient.PostAsJsonAsync(requestUrl, data);
        }

        public async Task<UserTokens> AuthenticateAndGetUserAsync(UserLoginCredentials userLoginCredentials)
        {
            //Post to login end point, get body content, read content as string, deserialize object to json object
            return JsonConvert.DeserializeObject<UserTokens>(
               await (await _httpClient.PostAsJsonAsync("/api/Users/login", userLoginCredentials))
                   .Content
                   .ReadAsStringAsync(), _settings);
        }

    }
}
