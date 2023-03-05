using Primitives3D;
using NUnit.Framework;

namespace PrimitivesTest;
{
    public sealed class VectorTests
    {
        [Test]
        public void Add_ShouldReturnCorrectVector()
        {
            // Arrange
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(4, 5, 6);

            // Act
            var result = Vector.Add(v1, v2);

            // Assert
            Assert.AreEqual(new Vector(5, 7, 9), result);
        }

        [Test]
        public void Subtract_ShouldReturnCorrectVector()
        {
            // Arrange
            var v1 = new Vector(4, 5, 6);
            var v2 = new Vector(1, 2, 3);

            // Act
            var result = Vector.Subtract(v1, v2);

            // Assert
            Assert.AreEqual(new Vector(3, 3, 3), result);
        }

        [Test]
        public void DotProduct_ShouldReturnCorrectValue()
        {
            // Arrange
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(4, 5, 6);

            // Act
            var result = Vector.DotProduct(v1, v2);

            // Assert
            Assert.AreEqual(32, result);
        }

        [Test]
        public void CrossProduct_ShouldReturnCorrectVector()
        {
            // Arrange
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(4, 5, 6);

            // Act
            var result = Vector.CrossProduct(v1, v2);

            // Assert
            Assert.AreEqual(new Vector(-3, 6, -3), result);
        }
    }
}
