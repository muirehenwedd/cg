namespace Primitives3D;

public struct Plane
{
    public Normal Normal { get; }

    public Plane(Normal normal)
    {
        Normal = normal;
    }
}