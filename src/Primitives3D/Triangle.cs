using Primitives3D.Abstractions;

namespace Primitives3D;

public struct Triangle : IRayIntersectable
{
    public Point A { get; }
    public Point B { get; }
    public Point C { get; }
    public Point[] Vertices => new[] {A, B, C};

    public Triangle(Point a, Point b, Point c)
    {
        A = a;
        B = b;
        C = c;
    }

    private Plane GetPlane()
    {
        var ab = B - A;
        var ac = C - A;
        var normal = Vector.Normalize(Vector.CrossProduct(ab, ac));

        return new Plane(new Normal(A, normal));
    }

    public Point? CalculateIntersectionsPoint(Ray ray)
    {
        var plane = GetPlane();
        var point = plane.CalculateIntersectionsPoint(ray);

        if (point == null)
        {
            return null;
        }

        var p = point.Value;

        var edge1 = B - A;
        var edge2 = C - B;
        var edge3 = A - C;

        var pToVertex1 = p - A;
        var pToVertex2 = p - B;
        var pToVertex3 = p - C;

        var cross1 = Vector.CrossProduct(edge1, pToVertex1);
        var cross2 = Vector.CrossProduct(edge2, pToVertex2);
        var cross3 = Vector.CrossProduct(edge3, pToVertex3);

        var normal = plane.Normal.Direction;

        if (Vector.DotProduct(normal, cross1) < 0 ||
            Vector.DotProduct(normal, cross2) < 0 ||
            Vector.DotProduct(normal, cross3) < 0)
        {
            return null;
        }

        return point;
    }

    public Plane? CalculateIntersectionsPlane(Ray ray)
    {
        var point = CalculateIntersectionsPoint(ray);

        if (point is null)
            return null;

        var planeNormalDirection = GetPlane().Normal.Direction;
        var prod = Vector.DotProduct(planeNormalDirection, ray.Direction);

        if (prod >= 0)
            planeNormalDirection = planeNormalDirection.Multiply(-1);

        return new Plane(new Normal(point.Value, planeNormalDirection));
    }
}