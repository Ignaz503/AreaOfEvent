using AreaOfEvent.Shared.Mathematics;
using System;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public class WireArc : Arc
    {
        public string StrokeStyle { get; set; }

        public WireArc()
        { }

        public WireArc( string strokeStyle, double startAngle, double endAngle, double radius, bool antiClockWise = false ) : base( startAngle, endAngle, radius, antiClockWise )
        {
            StrokeStyle = strokeStyle ?? throw new ArgumentNullException( nameof( strokeStyle ) );
        }

        public override async Task DrawAt( Vector2 position, RenderContext ctx )
        {
            await ctx.CanvasCTX.BeginPathAsync();

            await ctx.CanvasCTX.ArcAsync( position.X, position.Y, Radius, StartAngle, EndAngle, AntiClockWise );

            await ctx.CanvasCTX.SetStrokeStyleAsync( StrokeStyle );
            await ctx.CanvasCTX.StrokeAsync();
        }
    }
}
