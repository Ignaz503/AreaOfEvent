using AreaOfEvent.Shared.Mathematics;
using System;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public class Line : IShape
    {
        public string StrokeStyle { get; set; }

        public virtual Vector2 To { get; set; }

        public Line( string strokeStyle, Vector2 to )
        {
            StrokeStyle = strokeStyle ?? throw new ArgumentNullException( nameof( strokeStyle ) );
            To = to;
        }

        public Line( string strokeStyle )
        {
            StrokeStyle = strokeStyle ?? throw new ArgumentNullException( nameof( strokeStyle ) );
        }

        public Line()
        { }

        public async Task DrawAt( Vector2 position, RenderContext ctx )
        {
            await ctx.CanvasCTX.BeginPathAsync();

            await ctx.CanvasCTX.MoveToAsync( position.X, position.Y );
            await ctx.CanvasCTX.LineToAsync( To.X, To.Y );

            await ctx.CanvasCTX.SetStrokeStyleAsync( StrokeStyle );
            await ctx.CanvasCTX.StrokeAsync();
        }

        public bool Contains( Vector2 shapePosition, Vector2 checkAgainstPosition )
        {
            var dxc = checkAgainstPosition.X - shapePosition.X;
            var dyc = checkAgainstPosition.Y - shapePosition.Y;

            var dxl = To.X - shapePosition.X;
            var dyl = To.Y - shapePosition.Y;

            var cross = dxc * dyl - dyc * dxl;

            if (!Maths.AlmostEquals( cross, 0 ))
                return false;

            if (Math.Abs( dxl ) >= Math.Abs( dyl ))
                return dxl > 0 ?
                  shapePosition.X <= checkAgainstPosition.X && checkAgainstPosition.X <= To.X :
                  To.X <= checkAgainstPosition.X && checkAgainstPosition.X <= shapePosition.X;
            else
                return dyl > 0 ?
                  shapePosition.Y <= checkAgainstPosition.Y && checkAgainstPosition.Y <= To.Y :
                  To.Y <= checkAgainstPosition.Y && checkAgainstPosition.Y <= shapePosition.Y;
        }
    }
}
