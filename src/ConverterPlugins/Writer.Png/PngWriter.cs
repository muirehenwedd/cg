using System.IO;
using System.Linq;
using System.Text;
using Converter;
using Converter.Abstractions;

namespace Writer.Png
{
    public class PngWriter : IImageWriter
    {
        public string OutputFileExtension => "png";

        public byte[] Write(MediateImage image)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new BinaryWriter(memoryStream);

            // Write the PNG magic number
            writer.Write(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 });

            // Write the IHDR chunk
            var header = new PngHeader
            {
                Width = image.Width,
                Height = image.Height,
                BitDepth = 8,
                ColorType = PngColorType.Truecolor
            };
            var headerChunk = new PngChunk("IHDR", header.ToBytes());
            headerChunk.WriteToBinaryWriter(writer);

            // Write the IDAT chunk
            var imageData = PngEncoder.EncodeImageData(image.Pixels, image.Width, image.Height);
            var idatChunk = new PngChunk("IDAT", imageData);
            idatChunk.WriteToBinaryWriter(writer);

            // Write the IEND chunk
            var iendChunk = new PngChunk("IEND", new byte[] { });
            iendChunk.WriteToBinaryWriter(writer);

            return memoryStream.ToArray();
        }
    }
}
