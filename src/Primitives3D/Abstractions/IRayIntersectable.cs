namespace Primitives3D.Abstractions;

public interface IRayIntersectable
{
    Point[] CalculateIntersectionsPoints(Ray ray);
    (Point point, Normal normal)[] CalculateIntersectionsPointsWithNormals(Ray ray);
}