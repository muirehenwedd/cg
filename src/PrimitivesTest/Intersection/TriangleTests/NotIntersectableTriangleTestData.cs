using System.Collections;
using Primitives3D;

namespace PrimitivesTest.Intersection.TriangleTests;

public sealed class NotIntersectableTriangleTestData : IEnumerable<object[]>
{
    private static readonly (Triangle triangle, Ray ray)[] RawTestData =
    {
        (new Triangle(new Point(0, 0, 0), new Point(2, 0, 0), new Point(0, 2, 0)),
            new Ray(new Point(3, 3, -1), new Vector(0, 0, 1))),

        (new Triangle(new Point(-1, -1, 0), new Point(1, -1, 0), new Point(0, 1, 0)),
            new Ray(new Point(2, 2, -1), new Vector(0, 0, 1)))
    };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<object[]> GetEnumerator() =>
        RawTestData.Select(tuple => new object[] {tuple.triangle, tuple.ray}).GetEnumerator();
}