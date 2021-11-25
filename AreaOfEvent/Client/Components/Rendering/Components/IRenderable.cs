using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Components
{
    public interface IRenderable
    {
        Task Render( RenderContext ctx );
    }

}
