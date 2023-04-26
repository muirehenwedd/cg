using System.Text;
using System.Text.RegularExpressions;
using Converter;
using Converter.Abstractions;

namespace Reader.Ppm;

public sealed partial class PpmReader : IImageReader
{
    private string[] Normalize(string input)
    {
        var lines = input.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
        var text = string.Join(' ', lines.Where(line => !line.StartsWith("#")));

        return MyRegex().Replace(text, " ").Split();
    }

    public bool CanRead(byte[] bytes)
    {
        var text = Encoding.ASCII.GetString(bytes);
        var normalized = Normalize(text);

        if (normalized.Length < 3)
            return false;

        if (normalized[0] != "P3")
            return false;

        if (!int.TryParse(normalized[1], out var width))
            return false;

        if (!int.TryParse(normalized[2], out var height))
            return false;

        if (!int.TryParse(normalized[3], out var maximumValue))
            return false;

        if (normalized.Length - 4 != width * height * 3)
            return false;

        if (normalized.Skip(4).Any(s => !int.TryParse(s, out _)))
            return false;

        return true;
    }

    public MediateImage Read(byte[] bytes)
    {
        var text = Encoding.ASCII.GetString(bytes);
        var normalized = Normalize(text);

        var width = int.Parse(normalized[1]);
        var height = int.Parse(normalized[2]);

        var colorMaximumValue = int.Parse(normalized[3]);
        var multiplier = byte.MaxValue / colorMaximumValue;

        var mediateImage = new MediateImage(height, width);
        var inputPictureData = normalized.AsSpan(4);

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var pixelData = inputPictureData.Slice((width * i + j) * 3, 3);

                mediateImage.Pixels[i, j] = new Pixel(
                    NormalizeColorValue(int.Parse(pixelData[0])),
                    NormalizeColorValue(int.Parse(pixelData[1])),
                    NormalizeColorValue(int.Parse(pixelData[2]))
                );
            }
        }

        return mediateImage;

        byte NormalizeColorValue(int value)
        {
            return (byte) (value * multiplier);
        }
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex MyRegex();
}