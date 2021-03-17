using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using TCRS.APIAccess;
using TCRS.Business;
using TCRS.Shared.Contracts;

namespace TCRS.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            builder.Services.AddScoped<IPersistanceService, APIAccessService>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IUserService, UserService>();

            await builder.Build().RunAsync();
        }
    }
}
