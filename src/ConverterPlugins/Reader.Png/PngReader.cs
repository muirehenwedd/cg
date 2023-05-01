using System.IO;
using System.Linq;
using Converter;
using Converter.Abstractions;

namespace Reader.Png
{
    public class PngBitmapReader : IBitmapReader
    {
        public bool CanRead(byte[] bytes)
        {
            if (bytes.Length < 8) return false;

            var pngMagicNumber = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };
            return pngMagicNumber.SequenceEqual(bytes.Take(8));
        }

        public Bitmap Read(byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            using var reader = new BinaryReader(memoryStream);

            // Skip over the PNG magic number
            reader.ReadBytes(8);

            // Read the first chunk, which must be an IHDR chunk
            var header = PngHeader.ReadFromBinaryReader(reader);
            Assert.IsTrue(header.ChunkType == "IHDR");

            // Read the remaining chunks
            var chunks = new List<PngChunk>();
            while (true)
            {
                var chunk = PngChunk.ReadFromBinaryReader(reader);
                chunks.Add(chunk);
                if (chunk.ChunkType == "IEND") break;
            }

            // Find the IDAT chunks and concatenate them into a single byte array
            var imageData = chunks
                .Where(c => c.ChunkType == "IDAT")
                .Select(c => c.Data)
                .Aggregate((a, b) => a.Concat(b).ToArray());

            // Decode the image data
            var decodedData = PngDecoder.DecodeImageData(imageData, header.Width, header.Height, header.BitDepth, header.ColorType);

            // Create and return the bitmap
            return new Bitmap(decodedData, header.Width, header.Height);
        }
    }
}
