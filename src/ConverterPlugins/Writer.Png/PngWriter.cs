using System.Buffers.Binary;
using System.IO.Compression;
using System.Text;
using Converter;
using Converter.Abstractions;

namespace Writer.Png;

public class PngImageWriter : IImageWriter
{
    public string OutputFileExtension => "png";

    public byte[] Write(MediateImage image)
    {
        // PNG file signature (8 bytes)
        byte[] signature = {137, 80, 78, 71, 13, 10, 26, 10};

        // Create the PNG IHDR chunk
        var ihdrChunk = CreateIhdrChunk(image.Width, image.Height);

        // Create the PNG IDAT chunk
        var idatChunk = CreateIdatChunk(image);

        // Create the PNG IEND chunk
        var iendChunk = CreateIendChunk();

        // Combine all the chunks into a PNG file
        var pngData = CombineChunks(signature, ihdrChunk, idatChunk, iendChunk);

        return pngData;
    }

    private byte[] CreateIhdrChunk(int width, int height)
    {
        var chunkData = new byte[13];
        var chunkDataSpan = chunkData.AsSpan();

        BinaryPrimitives.WriteInt32BigEndian(chunkDataSpan[0..], width); // Image width

        BinaryPrimitives.WriteInt32BigEndian(chunkDataSpan[4..], height); // Image height
        chunkData[8] = 8; // Bit depth (8 bits per channel)
        chunkData[9] = 2; // Color type (2 = RGB)
        chunkData[12] = 0; // CRC

        return CreateChunk("IHDR", chunkData);
    }

    private byte[] CreateIdatChunk(MediateImage image)
    {
        var rowSize = image.Width * 3 + 1;
        var pixelData = new byte[rowSize * image.Height];
        var index = 0;

        for (var y = 0; y < image.Height; y++)
        {
            pixelData[index++] = 0; // Filter byte (None filter)
            for (var x = 0; x < image.Width; x++)
            {
                var pixel = image.Pixels[y, x];
                pixelData[index++] = pixel.R;
                pixelData[index++] = pixel.G;
                pixelData[index++] = pixel.B;
            }
        }

        byte[] compressedData;
        using (var memoryStream = new MemoryStream())
        {
            using (var deflateStream = new ZLibStream(memoryStream, CompressionMode.Compress))
            {
                deflateStream.Write(pixelData, 0, pixelData.Length);
            }
            compressedData = memoryStream.ToArray();
        }

        // Create the IDAT chunk with the compressed data
        return CreateChunk("IDAT", compressedData);
    }

    private byte[] CreateIendChunk()
    {
        return CreateChunk("IEND", Array.Empty<byte>());
    }

    private byte[] CreateChunk(string type, byte[] data)
    {
        var chunk = new byte[data.Length + 12];
        var chunkAsSpan = chunk.AsSpan();

        BinaryPrimitives.WriteInt32BigEndian(chunkAsSpan, data.Length); // Chunk length
        Encoding.ASCII.GetBytes(type).CopyTo(chunkAsSpan[4..]); // Chunk type
        data.CopyTo(chunkAsSpan[8..]); // Chunk data
        BinaryPrimitives.WriteInt32BigEndian(chunkAsSpan.Slice(data.Length + 8), 0); // CRC

        return chunk;
    }

    private byte[] CombineChunks(params byte[][] chunks)
    {
        var totalLength = chunks.Sum(chunk => chunk.Length);
        var pngData = new byte[totalLength];
        var offset = 0;
        foreach (var chunk in chunks)
        {
            Array.Copy(chunk, 0, pngData, offset, chunk.Length);
            offset += chunk.Length;
        }

        return pngData;
    }
}