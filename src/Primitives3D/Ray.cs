namespace Primitives3D;

public struct Ray
{
    public Point Origin { get; }
    public Vector Direction { get; }

    public Ray(Point start, Vector direction)
    {
        Origin = start;
        Direction = direction;
    }
    
    public Ray(Point start, Point direction)
    {
        Origin = start;
        Direction = new Vector(start, direction);
    }
    
    public Ray(Ray original)
    {
        Origin = original.Origin;
        Direction = original.Direction;
    }

    public Point GetNearestPointTo(Point point)
    {
        Vector diff = point - Origin;
        float projection = Vector.DotProduct(diff, Direction);
        if (projection <= 0)
        {
            return Origin;
        }
        else
        {
            return Origin + Direction * projection;
        }
    }
}
