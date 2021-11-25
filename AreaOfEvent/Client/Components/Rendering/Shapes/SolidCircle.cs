using AreaOfEvent.Shared.Mathematics;
using System;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public class SolidCircle : SolidArc
    {
        public SolidCircle( object fillStyle, string strokeStyle, double radius, bool antiClockwise = false ) : base( fillStyle, strokeStyle, 0, 2 * Math.PI, radius, antiClockwise )
        {
        }

        public override bool Contains( Vector2 shapePosition, Vector2 checkAgainstPosition )
        {
            return Maths.IsInside( checkAgainstPosition.X, checkAgainstPosition.Y, new() { X = shapePosition.X, Y = shapePosition.Y, Radius = Radius } );
        }
    }
}
