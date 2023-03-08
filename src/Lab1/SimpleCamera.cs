using Primitives3D;
using Primitives3D.Abstractions;

namespace Lab1;

public sealed class SimpleCamera : ICamera
{
    public required float PixelSize { get; init; }
    public Point ViewPoint { get; } = new Point(0, 0, 0);

    public Point[,] GetCentersOfPixels()
    {
        var halfPixelSize = PixelSize / 2;

        var pointsArray = new Point[20, 20];

        for (var i = 0; i < 20; i++)
        {
            for (var j = 0; j < 20; j++)
            {
                pointsArray[i, j] = new Point(5, 10 - j - halfPixelSize, 10 - i - halfPixelSize);
            }
        }

        return pointsArray;
    }
}