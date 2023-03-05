namespace Primitives3D;

public struct Vector
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    public static Vector Add(Vector left, Vector right)
    {
        float x = left.X + right.X;
        float y = left.Y + right.Y;
        float z = left.Z + right.Z;
        
        return new Vector(x, y, z);
    }

    public static Vector Subtract(Vector left, Vector right)
    {
        float x = left.X - right.X;
        float y = left.Y - right.Y;
        float z = left.Z - right.Z;

        return new Vector(x, y, z);
    }

    public static float DotProduct(Vector left, Vector right)
    {
        float x = left.X * right.X;
        float y = left.Y * right.Y;
        float z = left.Z * right.Z;

        return x + y + z;
    }

    public static float CrossProduct(Vector left, Vector right)
    {
        float x = left.Y * right.Z - left.Z * right.Y;
        float y = left.Z * right.X - left.X * right.Z;
        float z = left.X * right.Y - left.Y * right.X;

        return new Vector(x, y, z);
    }

    public Vector(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
