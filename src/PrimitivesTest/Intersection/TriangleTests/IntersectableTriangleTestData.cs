using System.Collections;
using Primitives3D;

namespace PrimitivesTest.Intersection.TriangleTests;

public sealed class IntersectableTriangleTestData : IEnumerable<object[]>
{
    private static readonly (Triangle triangle, Ray ray)[] RawTestData =
    {
        (new Triangle(new Point(0, 0, 0), new Point(2, 0, 0), new Point(0, 2, 0)),
            new Ray(new Point(1, 1, -1), new Vector(0, 0, 1))),

        (new Triangle(new Point(-1, -1, 0), new Point(1, -1, 0), new Point(0, 1, 0)),
            new Ray(new Point(0, 0, -1), new Vector(0, 0, 1))),

        (new Triangle(new Point(15, -5, -5), new Point(15, -7, -5), new Point(15, -5, -7)),
            new Ray(new Point(0, 0, 0), new Vector(15, -5, -5)))
    };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<object[]> GetEnumerator() =>
        RawTestData.Select(tuple => new object[] {tuple.triangle, tuple.ray}).GetEnumerator();
}