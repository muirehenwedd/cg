using System.Buffers.Binary;
using System.IO.Compression;
using Converter;
using Converter.Abstractions;

namespace Reader.Png;

public class PngImageReader : IImageReader
{
    public bool CanRead(byte[] bytes)
    {
        // Verify if the bytes start with the PNG file signature
        if (bytes.Length < 8)
            return false;

        var signature = new byte[] {137, 80, 78, 71, 13, 10, 26, 10};
        for (var i = 0; i < 8; i++)
        {
            if (bytes[i] != signature[i])
                return false;
        }

        return true;
    }

    public MediateImage Read(byte[] bytes)
    {
        var bytesAsSpan = bytes.AsSpan();

        var pixels = new List<Pixel>();

        // Read the IHDR chunk to extract image dimensions
        var chunkOffset = 8;
        var width = BinaryPrimitives.ReadInt32BigEndian(bytesAsSpan.Slice(chunkOffset + 8));
        var height = BinaryPrimitives.ReadInt32BigEndian(bytesAsSpan.Slice(chunkOffset + 12));

        // Read the IDAT chunks to extract compressed pixel data
        List<byte[]> idatChunks = ExtractChunks(bytes, "IDAT");
        if (idatChunks.Count == 0)
            throw new ArgumentException("Missing IDAT chunk.");

        var decompressedData = DecompressData(idatChunks);
        var expectedDataLength = (width * height * 3) + height; // Account for filter bytes

        // Check if the decompressed data length matches the expected length
        if (decompressedData.Length != expectedDataLength)
            throw new ArgumentException("Invalid PNG pixel data.");

        var dataIndex = 0;

        // Iterate over each row and extract pixel data
        for (var y = 0; y < height; y++)
        {
            var filterType = decompressedData[dataIndex++]; // Skip filter byte

            for (var x = 0; x < width; x++)
            {
                var r = decompressedData[dataIndex++];
                var g = decompressedData[dataIndex++];
                var b = decompressedData[dataIndex++];
                pixels.Add(new Pixel(r, g, b));
            }
        }

        // Create the MediateImage object with the extracted pixel data
        var image = new MediateImage(height, width);
        var pixelIndex = 0;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                image.Pixels[y, x] = pixels[pixelIndex++];
            }
        }

        return image;
    }

    private List<byte[]> ExtractChunks(byte[] bytes, string chunkType)
    {
        var chunks = new List<byte[]>();
        var offset = 8; // Skip PNG signature

        while (offset < bytes.Length - 12)
        {
            var length = BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(offset));
            var type = bytes.AsSpan(offset + 4, 4).ToString();

            if (type == chunkType)
            {
                var chunkData = new byte[length];
                Array.Copy(bytes, offset + 8, chunkData, 0, length);
                chunks.Add(chunkData);
            }

            offset += length + 12; // Skip the chunk and its CRC
        }

        return chunks;
    }

    private byte[] DecompressData(List<byte[]> chunks)
    {
        using var memoryStream = new MemoryStream();

        foreach (var chunk in chunks)
        {
            memoryStream.Write(chunk, 0, chunk.Length);
        }

        memoryStream.Position = 0;

        using var deflateStream = new ZLibStream(memoryStream, CompressionMode.Decompress);
        using var decompressedStream = new MemoryStream();

        deflateStream.CopyTo(decompressedStream);
        return decompressedStream.ToArray();
    }
}