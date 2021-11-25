using AreaOfEvent.Client.Services.GeoLocation.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Services.GeoLocation
{
    public interface IGeoLocationService
    {
        Task<BrowserGeoLocation> GetUserGeoLocationAsync();
    }
}
