using Converter;
using Converter.Abstractions;

namespace Writer.Bmp;

public class BmpWriter : IImageWriter
{
    public string OutputFileExtension => "bmp";

    public byte[] Write(MediateImage image)
    {
        var extraBytesPerRow = image.Width * 3 % 4 == 0 ? 0 : 4 - (image.Width * 3 % 4);
        var fileSize = BmpHeader.HeaderSize + BmpHeader.InfoSize + (image.Width * 3 + extraBytesPerRow) * image.Height;

        using var memoryStream = new MemoryStream();
        using var writer = new BinaryWriter(memoryStream);

        var header = new BmpHeader
        {
            FileSize = fileSize,
            Width = image.Width,
            Height = image.Height
        };

        header.WriteToBinaryWriter(writer);

        for (var i = image.Height - 1; i >= 0; i--)
        {
            for (var j = 0; j < image.Width; j++)
            {
                var pixel = image.Pixels[i, j];

                writer.Write(pixel.B);
                writer.Write(pixel.G);
                writer.Write(pixel.R);
            }

            for (var k = 0; k < extraBytesPerRow; k++)
            {
                writer.Write(byte.MinValue);
            }
        }

        return memoryStream.ToArray();
    }
}