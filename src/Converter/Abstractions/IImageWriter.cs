namespace Converter.Abstractions;

public interface IImageWriter
{
    string OutputFileExtension { get; }
    byte[] Write(MediateImage image);
}