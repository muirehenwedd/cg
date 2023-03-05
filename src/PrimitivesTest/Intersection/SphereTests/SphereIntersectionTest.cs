using Primitives3D;

namespace PrimitivesTest.Intersection.SphereTests;

public class SphereIntersectionTest
{
    [Theory]
    [ClassData(typeof(IntersectableTestData))]
    public void SphereShouldIntersectWithRay(
        Sphere sphere,
        Ray ray
    )
    {
        var points = sphere.CalculateIntersectionsPoints(ray);

        Assert.NotEmpty(points);
    }

    [Theory]
    [ClassData(typeof(NotIntersectableTestData))]
    public void SphereShouldNotIntersectWithRay(
        Sphere sphere,
        Ray ray
    )
    {
        var points = sphere.CalculateIntersectionsPoints(ray);

        Assert.Empty(points);
    }
}