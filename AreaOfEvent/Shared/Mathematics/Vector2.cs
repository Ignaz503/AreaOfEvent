namespace AreaOfEvent.Shared.Mathematics
{
    public struct Vector2
    {
        public double X;
        public double Y;

        public Vector2( double x, double y )
          => (X, Y) = (x, y);

        public Vector2( (double x, double y) pos )
            => (X, Y) = pos;

        public static Vector2 operator +( Vector2 lhs, Vector2 rhs )
            => new Vector2( lhs.X + rhs.X, lhs.Y + rhs.Y );

        public override string ToString()
            => $"{{x:{X}, y: {Y}}}";

    }
}
