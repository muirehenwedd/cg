using Primitives3D;

namespace Lab3;

public sealed class Transformation
{
    private readonly Matrix _transformationMatrix;

    public Transformation(Matrix transformationMatrix)
    {
        _transformationMatrix = transformationMatrix;
    }

    public Triangle Apply(Triangle input) => _transformationMatrix.Transform(input);
    public Point Apply(Point input) => _transformationMatrix.Transform(input);
    public Vector Apply(Vector input) => _transformationMatrix.Transform(input);
    public Normal Apply(Normal input) => _transformationMatrix.Transform(input);

    public void Apply(Triangle[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = Apply(input[i]);
        }
    }
    
    public void Apply(Point[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = Apply(input[i]);
        }
    }
    
    public void Apply(Vector[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = Apply(input[i]);
        }
    }
    
    public void Apply(Normal[] input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = Apply(input[i]);
        }
    }
}