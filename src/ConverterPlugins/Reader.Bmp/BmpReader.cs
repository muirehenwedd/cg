using Converter;
using Converter.Abstractions;

namespace Reader.Bmp;

public class BmpReader : IImageReader
{
    public bool CanRead(byte[] bytes)
    {
        using var reader = new BinaryReader(new MemoryStream(bytes));
        var header = BmpHeader.ReadFromBinaryReader(reader);

        if (header.B != 'B' || header.M != 'M')
            return false;

        if (header.Reserved != 0)
            return false;

        if (header.BiPlanes != 1)
            return false;

        if (header.Bits != 24)
            return false;

        return true;
    }

    public MediateImage Read(byte[] bytes)
    {
        using var memoryStream = new MemoryStream(bytes);
        using var reader = new BinaryReader(memoryStream);
        var header = BmpHeader.ReadFromBinaryReader(reader);

        memoryStream.Seek(header.HeaderSize, SeekOrigin.Begin);

        var mediateImage = new MediateImage(header.Height, header.Width);

        var extraBytesPerRow = header.Width * 3 % 4 == 0 ? 0 : 4 - (header.Width * 3 % 4);
        for (var i = header.Height - 1; i >= 0; i--)
        {
            for (var j = 0; j < header.Width; j++)
            {
                var b = reader.ReadByte();
                var g = reader.ReadByte();
                var r = reader.ReadByte();

                mediateImage.Pixels[i, j] = new Pixel(r, g, b);
            }

            for (var k = 0; k < extraBytesPerRow; k++)
            {
                reader.ReadByte();
            }
        }

        return mediateImage;
    }
}