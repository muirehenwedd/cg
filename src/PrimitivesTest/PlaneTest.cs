
namespace Primitives3D.Tests
{
    public class PlaneTests
    {
        [Fact]
        public void CalculateIntersectionsPoints_ReturnsCorrectResult()
        {
            // Arrange
            var plane = new Plane(new Normal(new Point(0, 0, 0), new Vector(0, 1, 0)));
            var ray1 = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));
            var ray2 = new Ray(new Point(0, 1, 0), new Vector(0, 1, 0));

            // Act
            var intersections1 = plane.CalculateIntersectionsPoints(ray1);
            var intersections2 = plane.CalculateIntersectionsPoints(ray2);

            // Assert
            Assert.Single(intersections1);
            Assert.Equal(new Point(0, 0, 0), intersections1[0]);
            Assert.Empty(intersections2);
        }

        [Fact]
        public void CalculateIntersectionsPlanes_ReturnsCorrectResult()
        {
            // Arrange
            var plane1 = new Plane(new Normal(new Point(0, 0, 0), new Vector(0, 1, 0)));
            var plane2 = new Plane(new Normal(new Point(0, 1, 0), new Vector(0, -1, 0)));
            var ray = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));

            // Act
            var intersections1 = plane1.CalculateIntersectionsPlanes(ray);
            var intersections2 = plane2.CalculateIntersectionsPlanes(ray);

            // Assert
            Assert.Single(intersections1);
            Assert.Equal(plane1.Normal, intersections1[0].Normal);
            Assert.Single(intersections2);
            Assert.Equal(new Normal(new Point(0, 1, 0), new Vector(0, 1, 0)), intersections2[0].Normal);
        }
    }
}
