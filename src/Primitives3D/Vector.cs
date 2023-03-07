namespace Primitives3D;

public struct Vector
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    public static Vector Add(Vector left, Vector right)
    {
        var x = left.X + right.X;
        var y = left.Y + right.Y;
        var z = left.Z + right.Z;
        
        return new Vector(x, y, z);
    }

    public static Vector Subtract(Vector left, Vector right)
    {
        var x = left.X - right.X;
        var y = left.Y - right.Y;
        var z = left.Z - right.Z;

        return new Vector(x, y, z);
    }

    public static float DotProduct(Vector left, Vector right)
    {
        var x = left.X * right.X;
        var y = left.Y * right.Y;
        var z = left.Z * right.Z;

        return x + y + z;
    }

    public static Vector CrossProduct(Vector left, Vector right)
    {
        var x = left.Y * right.Z - left.Z * right.Y;
        var y = left.Z * right.X - left.X * right.Z;
        var z = left.X * right.Y - left.Y * right.X;

        return new Vector(x, y, z);
    }
    public static Vector operator *(Vector v, float scalar)
    {
        var x = v.X * scalar;
        var y = v.Y * scalar;
        var z = v.Z * scalar;

        return new Vector(x, y, z);
    }
    public static Vector operator *(float scalar, Vector vector)
    {
        return vector * scalar;
    }
    public static Vector Normalize(Vector vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);

        if (length == 0)
        {
            throw new DivideByZeroException("Cannot normalize a zero-length vector.");
        }

        return new Vector(vector.X / length, vector.Y / length, vector.Z / length);
    }
    public Vector(Point start, Point direction) : this()
    {
        X = direction.X - start.X;
        Y = direction.Y - start.Y;
        Z = direction.Z - start.Z;
    }
    public Vector(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
}
