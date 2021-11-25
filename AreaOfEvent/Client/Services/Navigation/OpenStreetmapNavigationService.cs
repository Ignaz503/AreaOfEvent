using AreaOfEvent.Client.Services.Navigation.Data;
using AreaOfEvent.Shared.GeoLocation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Services.Navigation
{
    public class OpenStreetmapNavigationService : INavigationService
    {
        IJSRuntime jsRuntime;
        public OpenStreetmapNavigationService( IJSRuntime jsr )
        {
            this.jsRuntime = jsr;
        }

        public async Task NavigateTo( Location A, Location B, NavigationMode mode = NavigationMode.Car )
        {
            //car https://www.openstreetmap.org/directions?engine=fossgis_osrm_car&route=47.2360%2C15.6069%3B47.2107%2C15.6352
            // bike https://www.openstreetmap.org/directions?engine=fossgis_osrm_bike&route=47.2360%2C15.6069%3B47.2107%2C15.6352
            // https://www.openstreetmap.org/directions?engine=fossgis_osrm_foot&route=47.2360%2C15.6069%3B47.2107%2C15.6352
            string url = string.Empty;
            ;
            switch (mode)
            {
                case NavigationMode.Car:
                    url = $"https://www.openstreetmap.org/directions?engine=fossgis_osrm_car&route={A.Latitude}%2C{A.Longitude}%3B{B.Latitude}%2C{B.Longitude}";
                    break;
                case NavigationMode.Bycicle:
                    url = $"https://www.openstreetmap.org/directions?engine=fossgis_osrm_bike&route={A.Latitude}%2C{A.Longitude}%3B{B.Latitude}%2C{B.Longitude}";
                    break;
                case NavigationMode.Foot:
                    url = $"https://www.openstreetmap.org/directions?engine=fossgis_osrm_foot&route={A.Latitude}%2C{A.Longitude}%3B{B.Latitude}%2C{B.Longitude}";
                    break;
            }

            if (url != string.Empty)
            {
                await OpenURL( url );
            }

        }

        public async Task ShowMark( double latitude, double longitude )
        {
            //https://www.openstreetmap.org/?mlat={}&mlon={}#map=19/{}/{}&layers=N
            await OpenURL( $"https://www.openstreetmap.org/?mlat={latitude}&mlon={longitude}#map=19/{latitude}/{longitude}&layers=N" );
        }

        private async Task OpenURL( string url, string type = "_blank" )
        {
            //TODO why is there a ciruclar json exception here
            await jsRuntime.InvokeVoidAsync( "open", url, type );
        }

        public static void Register( IServiceCollection svCol, bool asBaseInterface = true )
        {
            if (asBaseInterface)
            {
                svCol.AddScoped<INavigationService>( provider => new OpenStreetmapNavigationService( provider.GetService<IJSRuntime>() ) );
            } else
            {
                svCol.AddScoped<OpenStreetmapNavigationService>( provider => new OpenStreetmapNavigationService( provider.GetService<IJSRuntime>() ) );
            }
        }

    }


}
