
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
            var intersections1 = disc.CalculateIntersectionsPoints(ray1);
            var intersections2 = disc.CalculateIntersectionsPoints(ray2);

            // Assert
            Assert.Single(intersections1);
            Assert.Equal(new Point(0, 1, 0), intersections1[0]);
            Assert.Empty(intersections2);
        }

        [Fact]
        public void CalculateIntersectionsPlanes_ReturnsCorrectResult()
        {
            // Arrange
            var plane = new Plane(new Normal(new Point(0, 0, 0), new Vector(0, 1, 0)));
            var disc = new Disc(plane, 1);
            var ray = new Ray(new Point(0, 2, 0), new Vector(0, -1, 0));

            // Act
            var intersections = disc.CalculateIntersectionsPlanes(ray);

            // Assert
            Assert.Single(intersections);
            Assert.Equal(plane.Normal, intersections[0].Normal);
        }
    }
}
