using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Components;
using AreaOfEvent.Client.Services.GeoLocation;
using AreaOfEvent.Shared.GeoLocation;
using System.Threading.Tasks;
using System.Timers;

namespace AreaOfEvent.Client.Components.Radars.Behaviours
{
    public class UserLocationTracker : Component, IUpdateable
    {
        IGeoLocationService geoLocationService;
        bool updateLocationFlag;

        Timer locationUpdateTimer;

        public Location Location { get; private set; }

        public bool IsDnyamiclyTracking => locationUpdateTimer != null;

        public UserLocationTracker( Location initialLocation, double updateInervalMilliseconds, IGeoLocationService geoLocationService, SceneObject containingObject ) : base( containingObject )
        {
            this.geoLocationService = geoLocationService;
            Location = initialLocation;
            if (updateInervalMilliseconds > 0)
            {
                locationUpdateTimer = new( updateInervalMilliseconds );
                locationUpdateTimer.AutoReset = true;
                locationUpdateTimer.Elapsed += SetupdateLocationFlag;
                locationUpdateTimer.Start();
            }
        }

        public async Task Update( UpdateContext ctx )
        {
            if (updateLocationFlag)
            {
                updateLocationFlag = false;
                var userLoc = await geoLocationService.GetUserGeoLocationAsync();
                if (userLoc.IsValid)
                {
                    Location = new() { Latitude = userLoc.Latitude, Longitude = userLoc.Longitude };
                }
            }
        }

        void SetupdateLocationFlag( object obj, ElapsedEventArgs args )
        {
            updateLocationFlag = true;
        }

        public override void Destroy()
        {
            locationUpdateTimer?.Stop();
            locationUpdateTimer?.Dispose();
            base.Destroy();
        }

    }
}
