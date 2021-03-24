using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using TCRS.APIAccess;
using TCRS.Business;
using TCRS.Client.AuthenticationStateProvider;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CitationManagement;

namespace TCRS.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddMudServices();

            //Add local storage service
            builder.Services.AddBlazoredLocalStorage();

            //Add HTTP client service for connecting to the API
            builder.Services.AddScoped(sp =>
                new HttpClient {BaseAddress = new Uri(builder.Configuration["apiURL"])});
            //Service for accessing server from client side
            builder.Services.AddScoped<IPersistenceService, APIAccessService>();
            //Services for managing user and maintaining information about user
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddTransient<ICitationManager, CitationManager>();

            //Load User Authentication services
            builder.Services.AddScoped<WebApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthenticationStateProvider>(
                provider => provider.GetRequiredService<WebApiAuthenticationStateProvider>());
            builder.Services.AddScoped<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider>(
                provider => provider.GetRequiredService<WebApiAuthenticationStateProvider>());

            await builder.Build().RunAsync();
        }
    }
}
