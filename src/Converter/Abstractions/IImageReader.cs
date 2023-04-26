namespace Converter.Abstractions;

public interface IImageReader
{
    bool CanRead(byte[] bytes);
    MediateImage Read(byte[] bytes);
}