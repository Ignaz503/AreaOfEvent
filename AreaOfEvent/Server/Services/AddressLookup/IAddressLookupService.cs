using AreaOfEvent.Server.Services.AddressLookup.Data;
using AreaOfEvent.Shared.GeoLocation;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Services.AddressLookup
{
    public interface IAddressLookupService
    {
        const string AddressLookupUserAgent = "NominatimUserAgent";

        Task<AddressLookupResult> LookupAddress( Location geoLocation );
    }
}
