using Primitives3D;
using Primitives3D.Abstractions;

public class Camera : ICamera
{
    public Point ViewPoint { get; private set; }
    public Vector ViewDirection { get; private set; }

    private readonly float _fieldOfView;
    private readonly int _resolutionWidth;
    private readonly int _resolutionHeight;

    public Camera(Point viewPoint, Vector viewDirection, float fieldOfView, int resolutionWidth, int resolutionHeight)
    {
        ViewPoint = viewPoint;
        ViewDirection = viewDirection;
        _fieldOfView = fieldOfView;
        _resolutionWidth = resolutionWidth;
        _resolutionHeight = resolutionHeight;
    }

    public Point[,] GetCentersOfPixels()
    {
        var usingFallback = false;
        var normalizedDirection = Vector.Normalize(ViewDirection);

        // define up vector
        var up = new Vector(0, 1, 0);

        // if the camera's view direction is parallel to the primary up vector, use the X-axis as the fallback
        if (Vector.AreParallel(normalizedDirection, up))
        {
            usingFallback = true;
            up = new Vector(1, 0, 0);
        }

        // calculate right vector
        var right = Vector.CrossProduct(normalizedDirection, up);
        right = Vector.Normalize(right);

        // recalculate up vector to make sure it's orthogonal to both direction and right vectors
        up = Vector.CrossProduct(right, normalizedDirection);

        var aspectRatio = (float) _resolutionWidth / _resolutionHeight;

        var centers = new Point[_resolutionWidth, _resolutionHeight];

        var tan = MathF.Tan(_fieldOfView / 180 * MathF.PI);

        for (var i = 0; i < _resolutionWidth; i++)
        {
            for (var j = 0; j < _resolutionHeight; j++)
            {
                var x = (1 - 2 * (i + 0.5f) / _resolutionWidth) * tan * aspectRatio;

                var y = (1 - 2 * (j + 0.5f) / _resolutionHeight) * tan;

                var direction = Vector.Normalize(right.Multiply(x) + up.Multiply(y) + normalizedDirection);

                centers[i, j] = new Point(ViewPoint.X + direction.X, ViewPoint.Y + direction.Y,
                    ViewPoint.Z + direction.Z);
            }
        }

        if (usingFallback)
            RotateArrayBy180(centers);

        return centers;
    }

    private static void RotateArrayBy180(Point[,] array)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        var rotatedArray = new Point[rows, cols];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                var rotatedRow = rows - 1 - i;
                var rotatedCol = cols - 1 - j;

                rotatedArray[rotatedRow, rotatedCol] = array[i, j];
            }
        }

        Array.Copy(rotatedArray, array, array.Length);
    }

    public void Transform(Matrix transformationMatrix)
    {
        ViewPoint = transformationMatrix.Transform(ViewPoint);
        ViewDirection = transformationMatrix.Transform(ViewDirection);
    }
}