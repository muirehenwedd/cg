using Primitives3D;

namespace PrimitivesTest.Intersection.TriangleTests;

public class TriangleIntersectionTest
{
    [Theory]
    [ClassData(typeof(IntersectableTriangleTestData))]
    public void TriangleShouldIntersectWithRay(
        Triangle triangle,
        Ray ray
    )
    {
        var point = triangle.CalculateIntersectionsPoint(ray);

        Assert.NotNull(point);
    }

    [Theory]
    [ClassData(typeof(NotIntersectableTriangleTestData))]
    public void TriangleShouldNotIntersectWithRay(
        Triangle triangle,
        Ray ray
    )
    {
        var point = triangle.CalculateIntersectionsPoint(ray);

        Assert.Null(point);
    }
}
