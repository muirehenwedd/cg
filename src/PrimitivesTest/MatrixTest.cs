using Primitives3D;

namespace PrimitivesTest;

using Xunit;

public class MatrixTests
{
    [Fact]
    public void MatrixShouldTranslatePoint()
    {
        var point = new Point(1, 2, 3);
        var matrix = Matrix.CreateTranslationMatrix(1, 2, 3);

        var result = matrix.Transform(point);

        Assert.Equal(new Point(2, 4, 6), result);
    }

    [Fact]
    public void MatrixShouldScalePoint()
    {
        var point = new Point(1, 2, 3);
        var matrix = Matrix.CreateScaleMatrix(2, 2, 2);

        var result = matrix.Transform(point);

        Assert.Equal(new Point(2, 4, 6), result);
    }

    [Fact]
    public void MatrixShouldRotatePointAroundX()
    {
        var point = new Point(1, 0, 0);
        var matrix = Matrix.CreateRotationXMatrix((float) Math.PI / 2);

        var result = matrix.Transform(point);

        Assert.Equal(new Point(1, 0, 0), result);
    }

    [Fact]
    public void MatrixShouldScaleVector()
    {
        var vector = new Vector(1, 2, 3);
        var matrix = Matrix.CreateScaleMatrix(2, 2, 2);

        var result = matrix.Transform(vector);

        Assert.Equal(new Vector(2, 4, 6), result);
    }

    [Fact]
    public void MatrixShouldRotateVectorAroundX()
    {
        var vector = new Vector(1, 0, 0);
        var matrix = Matrix.CreateRotationXMatrix((float) Math.PI / 2);

        var result = matrix.Transform(vector);

        Assert.Equal(new Vector(1, 0, 0), result);
    }

    [Fact]
    public void MatrixShouldTransformNormal()
    {
        var normal = new Normal(new Point(1, 0, 0), new Vector(1, 0, 0));
        var matrix = Matrix.CreateTranslationMatrix(1, 2, 3);

        var result = matrix.Transform(normal);

        Assert.Equal(new Normal(new Point(2, 2, 3), new Vector(1, 0, 0)), result);
    }

    [Fact]
    public void MatrixShouldTransformTriangle()
    {
        var triangle = new Triangle(new Point(0, 0, 0), new Point(1, 0, 0), new Point(0, 1, 0));
        var matrix = Matrix.CreateTranslationMatrix(1, 2, 3);

        var result = matrix.Transform(triangle);

        var expectedTriangle = new Triangle(new Point(1, 2, 3), new Point(2, 2, 3), new Point(1, 3, 3));
        Assert.Equal(expectedTriangle, result);
    }

    [Fact]
    public void CreateRotationYMatrix_ShouldCreateCorrectMatrix_ForGivenAngle()
    {
        var angleInDegrees = 45f;
        var angle = MathF.PI * angleInDegrees / 180.0f;

        var matrix = Matrix.CreateRotationYMatrix(angleInDegrees);

        Assert.Equal(Math.Cos(angle), matrix[0, 0], 3);
        Assert.Equal(Math.Sin(angle), matrix[0, 2], 3);
        Assert.Equal(1, matrix[1, 1]);
        Assert.Equal(-Math.Sin(angle), matrix[2, 0], 3);
        Assert.Equal(Math.Cos(angle), matrix[2, 2], 3);
        Assert.Equal(1, matrix[3, 3]);
    }

    [Fact]
    public void CreateRotationZMatrix_ShouldCreateCorrectMatrix_ForGivenAngle()
    {
        var angleInDegrees = 45f;
        var angle = MathF.PI * angleInDegrees / 180.0f;

        var matrix = Matrix.CreateRotationZMatrix(angleInDegrees);

        Assert.Equal(Math.Cos(angle), matrix[0, 0], 3);
        Assert.Equal(-Math.Sin(angle), matrix[0, 1], 3);
        Assert.Equal(Math.Sin(angle), matrix[1, 0], 3);
        Assert.Equal(Math.Cos(angle), matrix[1, 1], 3);
        Assert.Equal(1, matrix[2, 2]);
        Assert.Equal(1, matrix[3, 3]);
    }
}