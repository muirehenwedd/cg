namespace Primitives3D;

public struct Ray
{
    public Point Origin;
    public Vector Direction;

    public Ray(Point origin, Vector direction)
    {
        Origin = origin;
        Direction = direction;
    }

    public Ray(Point p1, Point p2)
    {
        Origin = p1;
        Direction = (p2 - p1).Normalize();
    }

    public Ray(Ray other)
    {
        Origin = other.Origin;
        Direction = other.Direction;
    }

    public Point GetNearestPointTo(Point point)
    {
        Vector diff = point - Origin;
        double projection = Vector.Dot(diff, Direction);
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
