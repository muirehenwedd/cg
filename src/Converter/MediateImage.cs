namespace Converter;

public sealed class MediateImage
{
    public Pixel[,] Pixels { get; }
    public int Width => Pixels.GetLength(1);
    public int Height => Pixels.GetLength(0);

    public MediateImage(int height, int width)
    {
        Pixels = new Pixel[height, width];
    }
}