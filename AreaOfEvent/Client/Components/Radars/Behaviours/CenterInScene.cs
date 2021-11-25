using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Components;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Radars.Behaviours
{
    public class CenterInScene : Component, IUpdateable
    {
        public CenterInScene( SceneObject containintObject ) : base( containintObject )
        { }

        public Task Update( UpdateContext ctx )
        {
            Transform.Position = new( ctx.Scene.GetCenter() );
            return Task.CompletedTask;
        }
    }
}
