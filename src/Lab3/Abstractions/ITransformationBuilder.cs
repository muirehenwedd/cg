namespace Lab3.Abstractions;

public interface ITransformationBuilder
{
    ITransformationBuilder Translate(float deltaX, float deltaY, float deltaZ);
    ITransformationBuilder Scale(float xScaleFactor, float yScaleFactor, float zScaleFactor);
    ITransformationBuilder RotateAroundX(float angleInDegrees);
    ITransformationBuilder RotateAroundY(float angleInDegrees);
    ITransformationBuilder RotateAroundZ(float angleInDegrees);

    Transformation BuildTransformation();
    ITransformationBuilder Reset();
}