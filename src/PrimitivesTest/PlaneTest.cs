
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
            var intersections1 = plane.CalculateIntersectionsPoint(ray1);
            var intersections2 = plane.CalculateIntersectionsPoint(ray2);

            // Assert
            Assert.NotNull(intersections1);
            Assert.Equal(new Point(0, 0, 0), intersections1);
            Assert.Null(intersections2);
        }

        [Fact]
        public void CalculateIntersectionsPlanes_ReturnsCorrectResult()
        {
            // Arrange
            var plane1 = new Plane(new Normal(new Point(0, 0, 0), new Vector(0, 1, 0)));
            var plane2 = new Plane(new Normal(new Point(0, 1, 0), new Vector(0, -1, 0)));
            var ray = new Ray(new Point(0, 1, 0), new Vector(0, -1, 0));

            // Act
            var intersections1 = plane1.CalculateIntersectionsPlane(ray);
            var intersections2 = plane2.CalculateIntersectionsPlane(ray);

            // Assert
            Assert.NotNull(intersections1);
            Assert.Equal(plane1.Normal, intersections1.Value.Normal);
            Assert.NotNull(intersections2);
            Assert.Equal(new Normal(new Point(0, 1, 0), new Vector(0, 1, 0)), intersections2.Value.Normal);
        }
    }
}
