namespace Primitives3D;

public struct Normal
{
        public Point Point { get; }
        public Vector Direction { get; }
        
        public Normal(Point point, Vector direction)
        {
            Point = point;
            Direction = Vector.Normalize(direction);
        }
}
