using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AreaOfEvent.Client.Services.GeoLocation.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace AreaOfEvent.Client.Services.GeoLocation
{
    public partial class GeoLocationService : IGeoLocationService
    {

        Dictionary<Guid,(TaskCompletionSource<BrowserGeoLocation>,DotNetObjectReference<GeoLocationService>)> requests = new();

        public IJSRuntime jsr;

        public GeoLocationService( IJSRuntime jsr )
        {
            this.jsr = jsr;
        }
        public async Task<BrowserGeoLocation> GetUserGeoLocationAsync()
        {
            var completionSource = new TaskCompletionSource<BrowserGeoLocation>();

            var id = Guid.NewGuid();
            var objRef = DotNetObjectReference.Create( this );
            requests.Add( id, (completionSource, objRef) );

            await jsr.InvokeVoidAsync( Constants.JsUserLocationFunction, objRef,
            id.ToString() );

            return await completionSource.Task;
        }

        [JSInvokable]
        public void Reply( string id, int code, double lat, double lon )
        {
            var guid = Guid.Parse(id);

            if (requests.TryGetValue( guid, out (TaskCompletionSource<BrowserGeoLocation>, DotNetObjectReference<GeoLocationService>) data ))
            {
                var (task, objRef) = data;
                task.SetResult( new() { Code = (BrowserGeoLocation.Status)code, Latitude = lat, Longitude = lon } );
                objRef.Dispose();
                requests.Remove( guid );
            } else
            {
                Console.WriteLine( $"no task with id {guid}" );
            }
        }

        public static void Register( IServiceCollection svCol, bool asBaseInterface = true )
        {
            if (asBaseInterface)
            {
                svCol.AddScoped<IGeoLocationService>( provider => new GeoLocationService( provider.GetService<IJSRuntime>() ) );
            } else
            {
                svCol.AddScoped( provider => new GeoLocationService( provider.GetService<IJSRuntime>() ) );
            }
        }

    }


}
