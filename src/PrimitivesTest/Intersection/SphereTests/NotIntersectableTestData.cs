using System.Collections;
using Primitives3D;

namespace PrimitivesTest.Intersection.SphereTests;

public sealed class NotIntersectableTestData : IEnumerable<object[]>
{
    private static readonly (Sphere sphere, Ray ray)[] RawTestData; // todo: add test data. Spheres must be not intersectable with rays.
    //= {(new Sphere(...), new Ray(...))};

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<object[]> GetEnumerator() =>
        RawTestData.Select(tuple => new object[] {tuple.sphere, tuple.ray}).GetEnumerator();
}