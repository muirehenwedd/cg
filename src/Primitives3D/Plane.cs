using Primitives3D.Abstractions;

namespace Primitives3D;

public struct Plane : IRayIntersectable
{
    public Normal Normal { get; }

    public Plane(Normal normal)
    {
        Normal = normal;
    }

    public Point[] CalculateIntersectionsPoints(Ray ray)
    {
        var prod = Vector.DotProduct(ray.Direction, Normal.Direction);

        if (prod < 1e-6 && prod > -1e-6)
        {
            return Array.Empty<Point>();
        }

        var p0l0 = Normal.Point - ray.Origin;
        var t = Vector.DotProduct(p0l0, Normal.Direction) / prod;
        if (t < 0)
            return Array.Empty<Point>();

        return new[] {ray.Origin + ray.Direction.Multiply(t)};
    }

    public Plane[] CalculateIntersectionsPlanes(Ray ray)
    {
        var prod = Vector.DotProduct(ray.Direction, Normal.Direction);

        if (prod < 1e-6 && prod > -1e-6)
        {
            return Array.Empty<Plane>();
        }

        var p0l0 = Normal.Point - ray.Origin;
        var t = Vector.DotProduct(p0l0, Normal.Direction) / prod;
        if (t < 0)
            return Array.Empty<Plane>();

        var point = ray.Origin + ray.Direction.Multiply(t);
        var direction = prod < 0 ? Normal.Direction : Normal.Direction.Multiply(-1);

        return new[]
        {
            new Plane(new Normal(point, direction))
        };
    }
}