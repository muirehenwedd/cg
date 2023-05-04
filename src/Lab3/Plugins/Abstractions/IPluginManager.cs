using Converter;

namespace Lab3.Plugins.Abstractions;

public interface IPluginManager
{
    bool WriterPluginExists(string formatName);
    byte[] WriteImage(MediateImage mediateImage, string formatName);
}