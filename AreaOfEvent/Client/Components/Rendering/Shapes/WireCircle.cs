using AreaOfEvent.Shared.Mathematics;
using System;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public class WireCircle : WireArc
    {
        public WireCircle( string strokeStyle, double radius, bool antiClockWise = false ) : base( strokeStyle, 0, 2 * Math.PI, radius, antiClockWise )
        {
        }

        public override bool Contains( Vector2 shapePosition, Vector2 checkAgainstPosition )
        {
            return Maths.IsInside( checkAgainstPosition.X, checkAgainstPosition.Y, new() { X = shapePosition.X, Y = shapePosition.Y, Radius = Radius } );
        }

    }
}
