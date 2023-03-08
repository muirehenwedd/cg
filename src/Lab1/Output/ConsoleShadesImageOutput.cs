using System.Text;
using Primitives3D.Abstractions;

namespace Lab1.Output;

public sealed class ConsoleShadesImageOutput : IImageOutput
{
    public void Output(float[,] imageMatrix)
    {
        var builder = new StringBuilder(imageMatrix.Length);

        for (var i = 0; i < imageMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < imageMatrix.GetLength(1); j++)
            {
                builder.Append(imageMatrix[i, j] switch
                {
                    <= 0f => ' ',
                    > 0f and < 0.2f => '.',
                    > 0.2f and < 0.5f => '*',
                    > 0.5f and < 0.8f => 'O',
                    _ => '#'
                });
            }

            if (i < imageMatrix.GetLength(0) - 1)
                builder.AppendLine();
        }

        Console.Write(builder.ToString());
    }
}