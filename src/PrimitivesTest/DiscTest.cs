
namespace Primitives3D.Tests
{
    public class DiscTests
    {
        [Fact]
        public void CalculateIntersectionsPoints_ReturnsCorrectResult()
        {
            // Arrange
            var plane = new Plane(new Normal(new Point(0, 0, 0), new Vector(0, 1, 0)));
            var disc = new Disc(plane, 1);
            var ray1 = new Ray(new Point(0, 2, 0), new Vector(0, -1, 0));
            var ray2 = new Ray(new Point(0, 2, 0), new Vector(0, 1, 0));

            // Act
            var intersection1 = disc.CalculateIntersectionsPoint(ray1);
            var intersection2 = disc.CalculateIntersectionsPoint(ray2);

            // Assert
            Assert.NotNull(intersection1);
            Assert.Equal(new Point(0, 0, 0), intersection1);
            Assert.Null(intersection2);
        }

        [Fact]
        public void CalculateIntersectionsPlanes_ReturnsCorrectResult()
        {
            // Arrange
            var plane = new Plane(new Normal(new Point(0, 0, 0), new Vector(0, 1, 0)));
            var disc = new Disc(plane, 1);
            var ray = new Ray(new Point(0, 2, 0), new Vector(0, -1, 0));

            // Act
            var intersections = disc.CalculateIntersectionsPlane(ray);

            // Assert
            Assert.NotNull(intersections);
            Assert.Equal(plane.Normal, intersections.Value.Normal);
        }
    }
}
