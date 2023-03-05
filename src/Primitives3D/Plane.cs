namespace Primitives3D;

public struct Plane
{
    public Normal Normal { get; }
    public Point Point { get; }

    public Plane(Normal normal, Point point)
    {
        Normal = normal;
        Point = point;
    }
}