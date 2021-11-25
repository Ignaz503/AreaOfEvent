using AreaOfEvent.Shared.Mathematics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public interface IShape
    {
        Task DrawAt( Vector2 position, RenderContext ctx );

        bool Contains( Vector2 shapePosition, Vector2 checkAgainstPosition );

    }
}
