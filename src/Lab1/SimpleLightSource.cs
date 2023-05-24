using Primitives3D;
using Primitives3D.Abstractions;

namespace Lab1;

public struct SimpleLightSource : ILightSource
{
    public Vector Direction { get; }

    public SimpleLightSource(Vector direction)
    {
        Direction = Vector.Normalize(direction).Multiply(-1);
    }
}