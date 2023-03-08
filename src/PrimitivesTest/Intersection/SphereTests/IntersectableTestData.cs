using System.Collections;
using Primitives3D;

namespace PrimitivesTest.Intersection.SphereTests;

public sealed class IntersectableTestData : IEnumerable<object[]>
{
    private static readonly (Sphere sphere, Ray ray)[] RawTestData =
    {
        (new Sphere(new Point(0, 0, 0), 2), new Ray(new Point(0, -1, 2), new Vector(0, 2.5f, -2))),
        (new Sphere(new Point(0, 0, 0), 4), new Ray(new Point(0, 0, 4), new Vector(1, 0, 0))),
        (new Sphere(new Point(0, 0, 0), 1), new Ray(new Point(0, 0, 0), new Vector(1, 0, 0)))
    };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<object[]> GetEnumerator() =>
        RawTestData.Select(tuple => new object[] {tuple.sphere, tuple.ray}).GetEnumerator();
}