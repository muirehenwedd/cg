using System.Collections;
using Primitives3D;

namespace PrimitivesTest.Intersection.SphereTests;

public sealed class NotIntersectableTestData : IEnumerable<object[]>
{
    private static readonly (Sphere sphere, Ray ray)[] RawTestData =
    {
        (new Sphere(new Point(0, 0, 0), 2), new Ray(new Point(0, 0, 2.1f), new Vector(0, 0, 1))),
        (new Sphere(new Point(3.1f, 0, 0), 3), new Ray(new Point(0, 0, 0), new Vector(0, 0, 1))),
        (new Sphere(new Point(0, 0, 0), 0.5f), new Ray(new Point(1, 0, 0), new Vector(1, 0, 0)))
    };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<object[]> GetEnumerator() =>
        RawTestData.Select(tuple => new object[] {tuple.sphere, tuple.ray}).GetEnumerator();
}