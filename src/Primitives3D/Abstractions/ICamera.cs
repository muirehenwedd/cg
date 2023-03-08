namespace Primitives3D.Abstractions;

public interface ICamera
{
    public Point ViewPoint { get; }
    Point[,] GetCentersOfPixels();
}