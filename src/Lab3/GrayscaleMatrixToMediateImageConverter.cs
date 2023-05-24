using Converter;
using Lab3.Abstractions;

namespace Lab3;

public sealed class GrayscaleMatrixToMediateImageConverter : IGrayscaleMatrixToMediateImageConverter
{
    public MediateImage Convert(float[,] matrix)
    {
        var image = new MediateImage(matrix.GetLength(0), matrix.GetLength(1));

        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                var matrixValue = matrix[i, j];

                if (matrixValue < 0)
                    matrixValue = 0;

                if (matrixValue > 1)
                    throw new Exception("Invalid matrix data.");

                var matrixValueByte = (byte) (matrixValue * 255);

                image.Pixels[i, j] = new Pixel(matrixValueByte, matrixValueByte, matrixValueByte);
            }
        }

        return image;
    }
}