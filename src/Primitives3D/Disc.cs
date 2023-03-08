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

    public Point[] CalculateIntersectionsPoints(Ray ray)
    {
        var intersectionsWithPlane = Plane.CalculateIntersectionsPoints(ray);

        if (intersectionsWithPlane.Length == 0)
            return intersectionsWithPlane;

        var point = intersectionsWithPlane[0];
        var distance = (Plane.Normal.Point - point).Length;

        if (distance <= Radius)
            return intersectionsWithPlane;

        return Array.Empty<Point>();
    }
   public Plane[] CalculateIntersectionsPlanes(Ray ray)
    {
        var intersectionsWithPlane = Plane.CalculateIntersectionsPlanes(ray);

        if (intersectionsWithPlane.Length == 0)
            return intersectionsWithPlane;

        var plane = intersectionsWithPlane[0];
        var distance = (Plane.Normal.Point - plane.Normal.Point).Length;

        if (distance <= Radius)
            return intersectionsWithPlane;

        return Array.Empty<Plane>();
    }
}