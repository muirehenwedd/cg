using Primitives3D.Abstractions;

namespace Primitives3D;

public struct Sphere : IRayIntersectable
{
    public Point Centre { get; }
    public Vector Radius { get; }

    public Sphere(Point centre, Vector radius)
    {
        Centre = centre;
        Radius = radius;
    }

    public Sphere(Point centre, float radius)
    {
        Centre = centre;
        Radius = new Vector(radius, 0f, 0f);
    }

    public Point? CalculateIntersectionsPoint(Ray ray)
    {
        var k = ray.Origin - Centre;

        var a = Vector.DotProduct(ray.Direction, ray.Direction);
        var b = 2 * Vector.DotProduct(ray.Direction, k);
        var c = Vector.DotProduct(k, k) - Vector.DotProduct(Radius, Radius);

        var discriminant = b * b - 4 * a * c;

        switch (discriminant)
        {
            case < 0:
                return null;
            case 0:
            {
                var root = -b / 2 * a;

                return root < 0 ? null : ray.Origin + ray.Direction.Multiply(root);
            }
            default:
            {
                var discriminantSquareRoot = MathF.Sqrt(discriminant);

                var root1 = (-b + discriminantSquareRoot) / (2 * a);
                var root2 = (-b - discriminantSquareRoot) / (2 * a);

                if (root1 < 0 && root2 < 0)
                    return null;

                if (root1 < 0)
                    return ray.Origin + ray.Direction.Multiply(root2);

                if (root2 < 0)
                    return ray.Origin + ray.Direction.Multiply(root1);

                var point1 = ray.Origin + ray.Direction.Multiply(root1);
                var point2 = ray.Origin + ray.Direction.Multiply(root2);

                return Point.CalculateDistance(ray.Origin, point1) < Point.CalculateDistance(ray.Origin, point2)
                    ? point1
                    : point2;
            }
        }
    }

    public Plane? CalculateIntersectionsPlane(Ray ray)
    {
        var k = ray.Origin - Centre;

        var a = Vector.DotProduct(ray.Direction, ray.Direction);
        var b = 2 * Vector.DotProduct(ray.Direction, k);
        var c = Vector.DotProduct(k, k) - Vector.DotProduct(Radius, Radius);

        var discriminant = b * b - 4 * a * c;

        switch (discriminant)
        {
            case < 0:
                return null;
            case 0:
            {
                var root = -b / 2 * a;

                return root < 0 ? null : CalculatePlane(root, Centre);
            }
            default:
            {
                var discriminantSquareRoot = MathF.Sqrt(discriminant);

                var root1 = (-b + discriminantSquareRoot) / (2 * a);
                var root2 = (-b - discriminantSquareRoot) / (2 * a);

                if (root1 < 0 && root2 < 0)
                    return null;

                if (root1 < 0)
                    return CalculatePlane(root2, Centre);

                if (root2 < 0)
                    return CalculatePlane(root1, Centre);

                var point1 = ray.Origin + ray.Direction.Multiply(root1);
                var point2 = ray.Origin + ray.Direction.Multiply(root2);

                return Point.CalculateDistance(ray.Origin, point1) < Point.CalculateDistance(ray.Origin, point2)
                    ? CalculatePlane(root1, Centre)
                    : CalculatePlane(root2, Centre);
            }
        }

        Plane CalculatePlane(float multiplyCoef, Point sphereCentre)
        {
            var point = ray.Origin + ray.Direction.Multiply(multiplyCoef);
            return new Plane(new Normal(point, Vector.Normalize(point - sphereCentre)));
        }
    }
}