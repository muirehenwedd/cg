using System.Text;
using Converter;
using Converter.Abstractions;

namespace Writer.Ppm;

public sealed class PpmWriter : IImageWriter
{
    public string OutputFileExtension => "ppm";

    public byte[] Write(MediateImage image)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"""
        P3
        {image.Width} {image.Height}
        255
        """);

        foreach (var (r, g, b) in image.Pixels)
        {
            builder.AppendLine($"""
            {r}
            {g}
            {b}
            """);
        }

        return Encoding.ASCII.GetBytes(builder.ToString());
    }
}