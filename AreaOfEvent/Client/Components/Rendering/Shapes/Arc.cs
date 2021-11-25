using AreaOfEvent.Shared.Mathematics;
using System;
using System.Threading.Tasks;

namespace AreaOfEvent.Client.Components.Rendering.Shapes
{
    public abstract class Arc : IShape
    {
        public double StartAngle { get; set; }
        public double EndAngle { get; set; }

        public double Radius { get; set; }

        public bool AntiClockWise { get; set; } = false;

        public Arc()
        { }

        protected Arc( double startAngle, double endAngle, double radius, bool antiClockWise = false )
        {
            StartAngle = startAngle;
            EndAngle = endAngle;
            Radius = radius;
            AntiClockWise = antiClockWise;
        }

        public virtual bool Contains( Vector2 shapePosition, Vector2 checkAgainstPosition )
        {
            Vector2 sectorStart = new(Math.Cos(StartAngle)*Radius+shapePosition.X,Math.Sin(StartAngle)*Radius + shapePosition.Y) ;
            Vector2 sectorEnd = new(Math.Cos(EndAngle)*Radius+shapePosition.X,Math.Sin(EndAngle)*Radius + shapePosition.Y) ;

            return !Maths.AreClockwise( sectorStart, checkAgainstPosition ) &&
                Maths.AreClockwise( sectorEnd, checkAgainstPosition ) &&
                Maths.IsWithinRadius( checkAgainstPosition, Radius );
        }

        public abstract Task DrawAt( Vector2 position, RenderContext ctx );


    }
}
