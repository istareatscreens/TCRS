using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using TCRS.APIAccess;
using TCRS.Shared.Contracts;
using TCRS.Shared.Objects.Auth;

namespace TCRS.Client.AuthenticationStateProvider
{
    //Used for only web API authentication
    public class WebApiAuthenticationStateProvider : Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider, IAuthenticationStateProvider
    {
        private readonly IUserService _currentUserServices;
        private readonly HttpClient _httpClient;
        private readonly LocalStorageService _localStorageService;
        private readonly APIAccessService _api;
        
        public WebApiAuthenticationStateProvider(
           IUserService currentUserServices ,
            HttpClient httpClient,
            IPersistenceService persistanceService,
            LocalStorageService localStorageService
            )
        {
            _currentUserServices = currentUserServices;
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            try
            {
                _api = (APIAccessService) persistanceService;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid cast: " + e);
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            try
            {
                 _httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("bearer", accessToken);
                var user = (await _api.GetAsync<User>(null)).ToList().First(); //call server end point to check token
                return CreateAuthenticationState(user);
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public void SetAuthenticatedState(UserTokens user)
        {
            var authStateTask = CreateAuthenticationState(user);
            NotifyAuthenticationStateChanged(authStateTask);
        }

        private AuthenticationState CreateAuthenticationState(User user)
        {
            _currentUserServices.User = user; //side effect updating user state
            var claimsPrincipal =
            //Add claims here and roles here
                new ClaimsPrincipal(new ClaimsIdentity(new[] {new Claim("id", "1")},"apiauth"));
            return new AuthenticationState(claimsPrincipal);

        }
        private async Task<AuthenticationState> CreateAuthenticationState(UserTokens tokens)
        {

            await _localStorageService.SetItemAsync("authToken",tokens.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokens.AccessToken);
            return CreateAuthenticationState(new User(tokens.AccessToken));

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
