using AreaOfEvent.Client.Components.Rendering.Components;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public class ShapeRenderer : Component, IRenderable
    {
        public IShape Shape { get; set; }

        public ShapeRenderer( SceneObject containingObject ) : base( containingObject )
        { }

        public ShapeRenderer( IShape shape, SceneObject containingObject ) : base( containingObject )
        {
            this.Shape = shape;
        }

        public async Task Render( RenderContext ctx )
        {
            await Shape?.DrawAt( Transform.Position, ctx );
        }
    }
}
