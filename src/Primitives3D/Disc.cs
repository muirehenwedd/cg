using Primitives3D.Abstractions;

namespace Primitives3D;

public struct Disc : IRayIntersectable
{
    public Plane Plane { get; }
    public float Radius { get; }

    public Disc(Plane plane, float radius)
    {
        Plane = plane;
        Radius = radius;
    }

    public Point? CalculateIntersectionsPoint(Ray ray)
    {
        var intersection = Plane.CalculateIntersectionsPoint(ray);

        if (intersection is null)
            return null;

        var distance = (Plane.Normal.Point - intersection.Value).Length;

        if (distance <= Radius)
            return intersection;

        return null;
    }
   public Plane? CalculateIntersectionsPlane(Ray ray)
    {
        var intersection = Plane.CalculateIntersectionsPlane(ray);

        if (intersection is null)
            return null;
        
        var distance = (Plane.Normal.Point - intersection.Value.Normal.Point).Length;

        if (distance <= Radius)
            return intersection;

        return null;
    }
}