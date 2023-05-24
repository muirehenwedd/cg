namespace Primitives3D;

public struct Point
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    public Point(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Point operator +(Point point, Vector vector)
    {
        var x = point.X + vector.X;
        var y = point.Y + vector.Y;
        var z = point.Z + vector.Z;
        return new Point(x, y, z);
    }

    public static Point operator -(Point point, Vector vector)
    {
        var x = point.X - vector.X;
        var y = point.Y - vector.Y;
        var z = point.Z - vector.Z;
        return new Point(x, y, z);
    }

    public static Vector operator -(Point p1, Point p2)
    {
        var x = p1.X - p2.X;
        var y = p1.Y - p2.Y;
        var z = p1.Z - p2.Z;

        return new Vector(x, y, z);
    }

    public float Square => X * X + Y * Y + Z * Z;

    public static float CalculateDistance(Point left, Point right)
    {
        return MathF.Sqrt(MathF.Pow(left.X - right.X, 2) + MathF.Pow(left.Y - right.Y, 2) +
                          MathF.Pow(left.X - right.X, 2));
    }

    public bool IsApproximatelyEqual(Point other, float tolerance = 1e-6f)
    {
        return Math.Abs(X - other.X) <= tolerance &&
               Math.Abs(Y - other.Y) <= tolerance &&
               Math.Abs(Z - other.Z) <= tolerance;
    }
}