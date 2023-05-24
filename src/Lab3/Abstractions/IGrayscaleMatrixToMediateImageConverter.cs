using Converter;

namespace Lab3.Abstractions;

public interface IGrayscaleMatrixToMediateImageConverter
{
    MediateImage Convert(float[,] matrix);
}