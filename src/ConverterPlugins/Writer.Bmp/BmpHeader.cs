namespace Writer.Bmp;

public sealed class BmpHeader
{
    public int FileSize { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public static readonly int HeaderSize = 54;
    public static readonly int InfoSize = 40;

    public void WriteToBinaryWriter(BinaryWriter writer)
    {
        writer.Write('B');
        writer.Write('M');
        writer.Write(FileSize);
        writer.Write(0);
        writer.Write(HeaderSize);
        writer.Write(InfoSize);
        writer.Write(Width);
        writer.Write(Height);
        writer.Write((short)1);
        writer.Write((short)24);
        writer.Write(0);
        writer.Write(0);
        writer.Write(0);
        writer.Write(0);
        writer.Write(0);
        writer.Write(0);
    }
}