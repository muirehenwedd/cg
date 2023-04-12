namespace Primitives3D.Abstractions;

public interface IRayIntersectable
{
    Point? CalculateIntersectionsPoint(Ray ray);
    Plane? CalculateIntersectionsPlane(Ray ray);
}