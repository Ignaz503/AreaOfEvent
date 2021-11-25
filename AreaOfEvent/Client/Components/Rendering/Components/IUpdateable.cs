using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Components
{
    public interface IUpdateable
    {
        Task Update( UpdateContext ctx );
    }

}
