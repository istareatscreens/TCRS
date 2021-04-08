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
            //var response = await _httpClient.GetFromJsonAsync<IList<T>>(requestUrl);
            var response = await _httpClient.GetAsync(requestUrl);
            await CheckHttpResponseMessage(response);
            return await response.Content.ReadFromJsonAsync<IList<T>>();
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
            var response = await _httpClient.PostAsJsonAsync(requestUrl, data);
            await CheckHttpResponseMessage(response);
            return JsonConvert.DeserializeObject<IEnumerable<U>>(
               await (response)
                   .Content
                   .ReadAsStringAsync(), _settings);
        }

        public async Task PutAsync<T>(T data)
        {
            var requestUrl = RouteByType.PutEntityRouteAssignment[typeof(T)];
            var response = await _httpClient.PutAsJsonAsync(requestUrl, data);
            await CheckHttpResponseMessage(response);
        }

        public async Task PostAsync<T>(T data)
        {
            await PostAsync<T>(data, new List<KeyValuePair<string, string>>());
        }

        public async Task PostAsync<T>(T data, List<KeyValuePair<string, string>> parametersList)
        {
            var requestUrl = RouteByType.PostEntityRouteAssignment[typeof(T)] + stringifyParameter(parametersList);
            var response = await _httpClient.PostAsJsonAsync(requestUrl, data);
            await CheckHttpResponseMessage(response);
        }

        public async Task<UserTokens> AuthenticateAndGetUserAsync(UserLoginCredentials userLoginCredentials)
        {
            //Post to login end point, get body content, read content as string, deserialize object to json object
            var response = await _httpClient.PostAsJsonAsync("/api/Users/login", userLoginCredentials);
            await CheckHttpResponseMessage(response);
            var result = JsonConvert.DeserializeObject<UserTokens>(
               await (response)
                   .Content
                   .ReadAsStringAsync(), _settings);
            return result;
        }

        private async Task CheckHttpResponseMessage(HttpResponseMessage response)
        {
            //If response is not successful provide error message from server
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await GetResponseMessage(response));
            }
        }

        private async Task<string> GetResponseMessage(HttpResponseMessage response)
        {
            try
            {
                return (await response.Content.ReadFromJsonAsync<ErrorString>()).Message;
            }
            catch
            {
                //Mask other errors
                return "Unknown Error";
            }
        }

    }

}
