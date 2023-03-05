using System;

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
            float x = point.X + vector.X;
            float y = point.Y + vector.Y;
            float z = point.Z + vector.Z;
            return new Point(x, y, z);
        }

        public static Point operator -(Point point, Vector vector)
        {
            float x = point.X - vector.X;
            float y = point.Y - vector.Y;
            float z = point.Z - vector.Z;
            return new Point(x, y, z);
        }
}
