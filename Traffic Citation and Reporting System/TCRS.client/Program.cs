using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TCRS.APIAccess;
using TCRS.Business;
using TCRS.Client.AuthStateProvider;
using TCRS.Client.BusyOverlay;
using TCRS.Shared.Contracts;
using TCRS.Shared.Contracts.CitationManagement;
using TCRS.Shared.Contracts.CourseManagement;
using TCRS.Shared.Contracts.EmployeeLookup;
using TCRS.Shared.Contracts.LookupPortal;
using TCRS.Shared.Contracts.Warrant;

namespace TCRS.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped(
                sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            //Busy Overlay
            builder.Services.AddScoped<BusyOverlayService>();

            //Add local storage service
            builder.Services.AddBlazoredLocalStorage();

            //Add HTTP client service for connecting to the API
            builder.Services.AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri(builder.Configuration["apiURL"]) });
            //Service for accessing server from client side
            builder.Services.AddScoped<IPersistenceService, APIAccessService>();
            //Services for managing user and maintaining information about user
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddTransient<ICitationManager, CitationManager>();
            builder.Services.AddTransient<ICourseManager, CourseManager>();
            builder.Services.AddTransient<IResolveCitationManager, ResolveCitationManager>();
            builder.Services.AddTransient<IEmployeeLookupManager, EmployeeLookupManager>();
            builder.Services.AddTransient<ILookupPortalManager, LookupPortalManager>();
            builder.Services.AddTransient<IWarrantManager, WarrantManager>();
            builder.Services.AddScoped<ICitationService, CitationService>();

            //Load User Authentication services
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<WebApiAuthStateProvider>();
            builder.Services.AddScoped<IAuthServiceProvider>(
                provider => provider.GetRequiredService<WebApiAuthStateProvider>());
            builder.Services.AddScoped<AuthenticationStateProvider>(
                provider => provider.GetRequiredService<WebApiAuthStateProvider>());
            /*
            builder.Services.AddScoped<IAuthServiceProvider>(
                provider => provider.GetRequiredService<WebApiAuthStateProvider>());
            */

            await builder.Build().RunAsync();
        }
    }
}
