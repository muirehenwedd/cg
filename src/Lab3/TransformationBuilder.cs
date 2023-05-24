using Lab3.Abstractions;
using Primitives3D;

namespace Lab3;

public sealed class TransformationBuilder : ITransformationBuilder
{
    private Matrix _transformationMatrix;

    public static TransformationBuilder Create()
    {
        return new TransformationBuilder();
    }

    private TransformationBuilder()
    {
        _transformationMatrix = Matrix.Identity;
    }

    public ITransformationBuilder Translate(float deltaX, float deltaY, float deltaZ)
    {
        var matrix = Matrix.CreateTranslationMatrix(deltaX, deltaY, deltaZ);

        _transformationMatrix = _transformationMatrix.Multiply(matrix);

        return this;
    }

    public ITransformationBuilder Scale(float xScaleFactor, float yScaleFactor, float zScaleFactor)
    {
        var matrix = Matrix.CreateScaleMatrix(xScaleFactor, yScaleFactor, zScaleFactor);

        _transformationMatrix = _transformationMatrix.Multiply(matrix);

        return this;
    }

    public ITransformationBuilder RotateAroundX(float angleInDegrees)
    {
        var matrix = Matrix.CreateRotationXMatrix(angleInDegrees);

        _transformationMatrix = _transformationMatrix.Multiply(matrix);

        return this;
    }

    public ITransformationBuilder RotateAroundY(float angleInDegrees)
    {
        var matrix = Matrix.CreateRotationYMatrix(angleInDegrees);

        _transformationMatrix = _transformationMatrix.Multiply(matrix);

        return this;
    }

    public ITransformationBuilder RotateAroundZ(float angleInDegrees)
    {
        var matrix = Matrix.CreateRotationZMatrix(angleInDegrees);

        _transformationMatrix = _transformationMatrix.Multiply(matrix);

        return this;
    }

    public Transformation BuildTransformation()
    {
        return new Transformation(_transformationMatrix);
    }

    public ITransformationBuilder Reset()
    {
        _transformationMatrix = Matrix.Identity;

        return this;
    }
}