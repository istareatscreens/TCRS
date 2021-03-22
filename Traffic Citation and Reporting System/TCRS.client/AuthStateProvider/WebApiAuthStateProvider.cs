using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using TCRS.APIAccess;
using TCRS.Shared.Contracts;
using TCRS.Shared.Helper;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Client.AuthStateProvider
{
    //Used for only web API authentication
    public class WebApiAuthStateProvider : AuthenticationStateProvider, IAuthServiceProvider
    {
        private readonly IUserService _currentUserServices;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly APIAccessService _api;

        public WebApiAuthStateProvider(
           IUserService currentUserServices,
            HttpClient httpClient,
            IPersistenceService persistenceService,
            ILocalStorageService localStorageService
            )
        {
            _currentUserServices = currentUserServices;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            try
            {
                _api = (APIAccessService)persistenceService;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid cast: " + e);
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string accessToken = "";
            try
            {
                accessToken = await _localStorageService.GetItemAsync<string>("authToken");

                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    Console.WriteLine("FAILED To REtrieve");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                _httpClient.DefaultRequestHeaders.Authorization
                   = new AuthenticationHeaderValue("bearer", accessToken);
                var user = (await _api.GetAsync<User>()).ToList().First(); //call server end point to check token
                _currentUserServices.User = user; //assign user to userservice
                return await CreateAuthenticationState(new UserTokens { AccessToken = accessToken });
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public void SetAuthenticatedState(UserTokens user)
        {
            _currentUserServices.User = new User(user.AccessToken); //side effect updating user state
            var authStateTask = CreateAuthenticationState(user);
            NotifyAuthenticationStateChanged(authStateTask);
        }

        private async Task<AuthenticationState> CreateAuthenticationState(UserTokens tokens)
        {
            await _localStorageService.SetItemAsync("authToken", tokens.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokens.AccessToken);
            var claimsPrincipal =
                new ClaimsPrincipal(new ClaimsIdentity(JwtParser.GetClaimsFromJWT(tokens.AccessToken), "jwtAuthType"));
            return new AuthenticationState(claimsPrincipal);

            /*
            await _localStorageService.SetItemAsync("authToken", user.Token);
            _currentUserService.CurrentUser = user;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.Token);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(user.Token);

            var claims = token.Claims.ToList();
            var roleClaim = claims.FirstOrDefault(c => c.Type == "role");
            if (roleClaim != null)
            {
                var newRoleClaim = new Claim(ClaimTypes.Role, roleClaim.Value);
                claims.Add(newRoleClaim);
            }

             Console.WriteLine(JsonSerializer.Serialize(claims));

            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(claims, "apiauth"));

            //var claimsPrincipal = new ClaimsPrincipal(
            //    new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }, "apiauth"));
            return new AuthenticationState(claimsPrincipal);
            */
        }

        public void UnsetUser()
        {
            NotifyAuthenticationStateChanged(CreateUnsetUserAuthenticationStateAsync());
        }

        private async Task<AuthenticationState> CreateUnsetUserAuthenticationStateAsync()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            var unsetUser = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(unsetUser);
        }

    }
}
