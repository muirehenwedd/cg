namespace Primitives3D.Abstractions;

public interface IRayIntersectable
{
    Point[] CalculateIntersectionsPoints(Ray ray);
}