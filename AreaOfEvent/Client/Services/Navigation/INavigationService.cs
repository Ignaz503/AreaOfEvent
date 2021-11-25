using System.Threading.Tasks;
using AreaOfEvent.Client.Services.Navigation.Data;
using AreaOfEvent.Shared.GeoLocation;

namespace AreaOfEvent.Client.Services.Navigation
{
    public interface INavigationService
    {
        Task ShowMark( double latitude, double longitude );


        Task NavigateTo( Location A, Location B, NavigationMode mode = NavigationMode.Car );
    }
}
