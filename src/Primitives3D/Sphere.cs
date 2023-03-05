using Primitives3D.Abstractions;

namespace Primitives3D;

public struct Sphere : IRayIntersectable
{
    public Sphere(Point centre, float radius)
    {
        // todo: initialize sphere, add fields if needed.
    }

    public Point[] CalculateIntersectionsPoints(Ray ray)
    {
        // todo: calculate the intersection points of the sphere and ray. Return 'Array.Empty<Point>' if no points were found.
        throw new NotImplementedException();
    }

    public (Point point, Normal normal)[] CalculateIntersectionsPointsWithNormals(Ray ray)
    {
        // todo
        throw new NotImplementedException();
    }
}