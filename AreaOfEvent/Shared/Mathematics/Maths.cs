using System;

namespace AreaOfEvent.Shared.Mathematics
{
    public static class Maths
    {
        public const double DegToRad =  Math.PI / 180.0d;

        public const double RadToDeg = 180.0d / Math.PI;

        public static double Lerp( double v0, double v1, double t )
            => ((1.0d - t) * v0) + (t * v1);
        public static bool IsInside( double x, double y, Circle circle )
        {
            return ((x - circle.X) * (x - circle.X)) + ((y - circle.Y) * (y - circle.Y)) < (circle.Radius * circle.Radius);
        }

        public static double ClampAngleDegree( double angleInDeg )
        {
            return ModularClamp( angleInDeg, 0, 360, 0, 360 );
        }
        public static double ModularClamp( double val, double min, double max, double rangemin, double rangemax )
        {
            var modulus = Math.Abs(rangemax - rangemin);
            if ((val %= modulus) < 0f)
                val += modulus;
            return Math.Clamp( val + Math.Min( rangemin, rangemax ), min, max );
        }

        public static bool AlmostEquals( double value1, double value2, double epsilon = 0.0000001 )
        {
            return Math.Abs( value1 - value2 ) < epsilon;
        }

        public static bool AreClockwise( Vector2 v1, Vector2 v2 )
        {
            return -v1.X * v2.Y + v1.Y * v2.X > 0;
        }
        public static bool IsWithinRadius( Vector2 v, double radius )
        {
            return v.X * v.X + v.Y * v.Y <= (radius * radius);
        }
    }

}
