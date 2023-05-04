namespace Primitives3D;

public class Matrix
{
    private readonly float[,] _values;

    public Matrix()
    {
        _values = new float[4, 4];
    }

    public float this[int row, int col]
    {
        get => _values[row, col];
        set => _values[row, col] = value;
    }

    public static Matrix CreateTranslationMatrix(float tx, float ty, float tz)
    {
        var result = new Matrix();
        for (var i = 0; i < 4; i++)
        {
            result[i, i] = 1;
        }

        result[0, 3] = tx;
        result[1, 3] = ty;
        result[2, 3] = tz;

        return result;
    }

    public static Matrix CreateScaleMatrix(float sx, float sy, float sz)
    {
        var result = new Matrix();
        result[0, 0] = sx;
        result[1, 1] = sy;
        result[2, 2] = sz;
        result[3, 3] = 1;

        return result;
    }

    // Create rotation matrix around X axis
    public static Matrix CreateRotationXMatrix(float angleInDegrees)
    {
        var angle = MathF.PI * angleInDegrees / 180.0f;

        var result = new Matrix();
        var cos = MathF.Cos(angle);
        var sin = MathF.Sin(angle);

        result[0, 0] = 1;
        result[1, 1] = cos;
        result[1, 2] = -sin;
        result[2, 1] = sin;
        result[2, 2] = cos;
        result[3, 3] = 1;

        return result;
    }

    public static Matrix CreateRotationYMatrix(float angleInDegrees)
    {
        var angle = MathF.PI * angleInDegrees / 180.0f;

        var result = new Matrix();
        var cos = MathF.Cos(angle);
        var sin = MathF.Sin(angle);

        result[0, 0] = cos;
        result[0, 2] = sin;
        result[1, 1] = 1;
        result[2, 0] = -sin;
        result[2, 2] = cos;
        result[3, 3] = 1;

        return result;
    }

    public static Matrix CreateRotationZMatrix(float angleInDegrees)
    {
        var angle = MathF.PI * angleInDegrees / 180.0f;

        var result = new Matrix();
        var cos = MathF.Cos(angle);
        var sin = MathF.Sin(angle);

        result[0, 0] = cos;
        result[0, 1] = -sin;
        result[1, 0] = sin;
        result[1, 1] = cos;
        result[2, 2] = 1;
        result[3, 3] = 1;

        return result;
    }

    // Apply this matrix to a point
    public Point Transform(Point point)
    {
        var x = this[0, 0] * point.X + this[0, 1] * point.Y + this[0, 2] * point.Z + this[0, 3];
        var y = this[1, 0] * point.X + this[1, 1] * point.Y + this[1, 2] * point.Z + this[1, 3];
        var z = this[2, 0] * point.X + this[2, 1] * point.Y + this[2, 2] * point.Z + this[2, 3];

        return new Point(x, y, z);
    }

    // Apply this matrix to a vector
    public Vector Transform(Vector vector)
    {
        var x = this[0, 0] * vector.X + this[0, 1] * vector.Y + this[0, 2] * vector.Z;
        var y = this[1, 0] * vector.X + this[1, 1] * vector.Y + this[1, 2] * vector.Z;
        var z = this[2, 0] * vector.X + this[2, 1] * vector.Y + this[2, 2] * vector.Z;

        return new Vector(x, y, z);
    }

    // Apply this matrix to a normal
    public Normal Transform(Normal normal)
    {
        var transformedVector = Transform(normal.Direction);
        return new Normal(Transform(normal.Point), transformedVector);
    }

    // Apply this matrix to a triangle
    public Triangle Transform(Triangle triangle)
    {
        var transformedVertices = triangle.Vertices.Select(Transform).ToArray();

        return new Triangle(transformedVertices[0], transformedVertices[1], transformedVertices[2]);
    }
}