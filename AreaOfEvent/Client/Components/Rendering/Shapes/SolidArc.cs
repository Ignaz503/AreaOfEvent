using AreaOfEvent.Shared.Mathematics;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public class SolidArc : Arc
    {
        public object FillStyle { get; set; }
        public string StrokeStyle { get; set; }

        public SolidArc( object fillStyle, string strokeStyle, double startAngle, double endAngle, double radius, bool antiClockWise = false ) : base( startAngle, endAngle, radius, antiClockWise )
        {
            FillStyle = fillStyle;
            StrokeStyle = strokeStyle;
        }

        public override async Task DrawAt( Vector2 position, RenderContext ctx )
        {
            await ctx.CanvasCTX.BeginPathAsync();

            await ctx.CanvasCTX.ArcAsync( position.X, position.Y, Radius, StartAngle, EndAngle, AntiClockWise );
            await ctx.CanvasCTX.SetStrokeStyleAsync( StrokeStyle );
            await ctx.CanvasCTX.SetFillStyleAsync( FillStyle );
            await ctx.CanvasCTX.FillAsync();
        }
    }
}
