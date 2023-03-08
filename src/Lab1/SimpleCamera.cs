using Primitives3D;
using Primitives3D.Abstractions;

namespace Lab1;

public sealed class SimpleCamera : ICamera
{
    private float _screenSize;
    private float _screenDistance;
    private int _pixelsOnOneSide;
    private float _pixelSize;

    public SimpleCamera(float screenDistance, float fieldOfViewDegrees, int pixelsOnOneSide)
    {
        _screenSize = MathF.Tan(fieldOfViewDegrees / 180 * MathF.PI) * screenDistance * 2;

        _screenDistance = screenDistance;
        _pixelsOnOneSide = pixelsOnOneSide;

        _pixelSize = _screenSize / pixelsOnOneSide;
    }

    public Point ViewPoint { get; } = new Point(0, 0, 0);

    public Point[,] GetCentersOfPixels()
    {
        var halfPixelSize = _pixelSize / 2;
        var halfScreenSize = _screenSize / 2;

        var pointsArray = new Point[_pixelsOnOneSide, _pixelsOnOneSide];

        var startCoordinate = halfScreenSize - halfPixelSize;

        for (var i = 0; i < _pixelsOnOneSide; i++)
        {
            for (var j = 0; j < _pixelsOnOneSide; j++)
            {
                pointsArray[i, j] = new Point(_screenDistance, startCoordinate - _pixelSize * j,
                    startCoordinate - _pixelSize * i);
            }
        }

        return pointsArray;
    }
}