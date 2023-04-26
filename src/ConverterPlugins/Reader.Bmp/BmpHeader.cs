namespace Reader.Bmp;

public struct BmpHeader
{
    public char B;
    public char M;
    public int FileSize;
    public int Reserved;
    public int HeaderSize;
    public int InfoSize;
    public int Width;
    public int Height;
    public short BiPlanes;
    public short Bits;
    public int BiCompression;
    public int BiSizeImage;
    public int BiXPelsPerMeter;
    public int BiYPelsPerMeter;
    public int BiClrUsed;
    public int BiClrImportant;

    public static BmpHeader ReadFromBinaryReader(BinaryReader reader) =>
        new BmpHeader
        {
            B = reader.ReadChar(),
            M = reader.ReadChar(),
            FileSize = reader.ReadInt32(),
            Reserved = reader.ReadInt32(),
            HeaderSize = reader.ReadInt32(),
            InfoSize = reader.ReadInt32(),
            Width = reader.ReadInt32(),
            Height = reader.ReadInt32(),
            BiPlanes = reader.ReadInt16(),
            Bits = reader.ReadInt16(),
            BiCompression = reader.ReadInt32(),
            BiSizeImage = reader.ReadInt32(),
            BiXPelsPerMeter = reader.ReadInt32(),
            BiYPelsPerMeter = reader.ReadInt32(),
            BiClrUsed = reader.ReadInt32(),
            BiClrImportant = reader.ReadInt32()
        };
}