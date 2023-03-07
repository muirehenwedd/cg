namespace Primitives3D.Abstractions;

public interface IRayIntersectable
{
    Point[] CalculateIntersectionsPoints(Ray ray);
    Plane[] CalculateIntersectionsPlanes(Ray ray);
}