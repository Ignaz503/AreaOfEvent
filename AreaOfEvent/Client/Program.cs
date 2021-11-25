using AreaOfEvent.Client.Services.GeoLocation;
using AreaOfEvent.Client.Services.Navigation;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Client
{
    public class Program
    {
        public static async Task Main( string[] args )
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>( "#app" );

            builder.Services.AddHttpClient( "AreaOfEvent.ServerAPI", client => client.BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) )
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped( sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient( "AreaOfEvent.ServerAPI" ) );

            builder.Services.AddApiAuthorization();

            GeoLocationService.Register( builder.Services );
            OpenStreetmapNavigationService.Register( builder.Services );

            builder.Services.AddBlazoredModal();

            await builder.Build().RunAsync();
        }
    }
}
