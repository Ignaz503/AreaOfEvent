using AreaOfEvent.Client.Components.Rendering;
using AreaOfEvent.Client.Components.Rendering.Components;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Radars.Behaviours
{
    public class RadarBackgroundCross : Component, IRenderable, IRadarElement
    {
        public string StrokeStyle { get; set; }
        public RadarSettings Settings { get; private set; }

        public RadarBackgroundCross( RadarSettings s, SceneObject containingObject ) : base( containingObject )
        {
            Settings = s;
        }

        public async Task Render( RenderContext ctx )
        {
            var maxRadius = Settings.GetMaxRadius(ctx.Scene.Width,ctx.Scene.Height);

            double lineXStart = SceneObject.Transform.Position.X - maxRadius;
            double lineYStart = SceneObject.Transform.Position.Y - maxRadius;

            double lineXEnd = SceneObject.Transform.Position.X + maxRadius ;
            double lineYEnd = SceneObject.Transform.Position.Y + maxRadius ;

            await ctx.CanvasCTX.BeginPathAsync();

            await ctx.CanvasCTX.SetStrokeStyleAsync( StrokeStyle );

            await ctx.CanvasCTX.MoveToAsync( lineXStart, SceneObject.Transform.Position.Y );
            await ctx.CanvasCTX.LineToAsync( lineXEnd, SceneObject.Transform.Position.Y );
            await ctx.CanvasCTX.StrokeAsync();

            await ctx.CanvasCTX.BeginPathAsync();
            await ctx.CanvasCTX.MoveToAsync( SceneObject.Transform.Position.X, lineYStart );
            await ctx.CanvasCTX.LineToAsync( SceneObject.Transform.Position.X, lineYEnd );
            await ctx.CanvasCTX.StrokeAsync();
        }
    }

}
