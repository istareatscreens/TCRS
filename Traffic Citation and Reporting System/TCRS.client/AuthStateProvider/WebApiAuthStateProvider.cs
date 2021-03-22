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
                    throw new Exception();
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
               new ClaimsPrincipal(new ClaimsIdentity(JwtParser.GetClaimsFromJWT(tokens.AccessToken).ToArray(), "jwtAuthType"));
            return new AuthenticationState(claimsPrincipal);
        }

        public void UnsetUser()
        {
            NotifyAuthenticationStateChanged(CreateUnsetUserAuthenticationStateAsync());
        }

        private async Task<AuthenticationState> CreateUnsetUserAuthenticationStateAsync()
        {
            try
            {
                await _localStorageService.RemoveItemAsync("authToken");
            }
            catch
            {

            }
            var unsetUser = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(unsetUser);
        }

    }
}
