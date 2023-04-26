using Converter;

namespace Lab2.Plugins.Abstractions;

public interface IPluginManager
{
    bool WriterPluginExists(string formatName);
    bool TryReadImage(byte[] bytes, out MediateImage? mediateImage);
    byte[] WriteImage(MediateImage mediateImage, string formatName);
}