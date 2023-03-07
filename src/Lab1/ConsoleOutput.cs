using System.Text;

namespace Lab1;

public static class ConsoleOutput
{
    public static void BinaryDisplay(float[,] imageMatrix)
    {
        var builder = new StringBuilder(imageMatrix.Length);

        for (var i = 0; i < imageMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < imageMatrix.GetLength(1); j++)
            {
                builder.Append(imageMatrix[i, j] >= 1 ? '#' : ' ');
            }

            if (i < imageMatrix.GetLength(0) - 1)
                builder.AppendLine();
        }

        Console.Write(builder.ToString());
    }

    public static void ShadesDisplay(float[,] imageMatrix)
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