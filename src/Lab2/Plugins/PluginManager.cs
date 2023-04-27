using System.Reflection;
using Converter;
using Converter.Abstractions;
using Lab2.Plugins.Abstractions;

namespace Lab2.Plugins;

public sealed class PluginManager : IPluginManager
{
    private readonly IImageWriter[] _imageWriters;
    private readonly IImageReader[] _imageReaders;

    public PluginManager(IPluginLoader pluginLoader)
    {
        _imageReaders = pluginLoader.LoadReaders();
        _imageWriters = pluginLoader.LoadWriters();
    }

    public bool WriterPluginExists(string formatName)
    {
        return _imageWriters.Any(w =>
            w.OutputFileExtension.Equals(formatName, StringComparison.InvariantCultureIgnoreCase));
    }

    public bool TryReadImage(byte[] bytes, out MediateImage? mediateImage)
    {
        var reader = _imageReaders.FirstOrDefault(r => r!.CanRead(bytes));

        if (reader is null)
        {
            mediateImage = default;
            return false;
        }

        mediateImage = reader.Read(bytes);
        return true;
    }

    public byte[] WriteImage(MediateImage mediateImage, string formatName)
    {
        var writer = _imageWriters.First(w =>
            w.OutputFileExtension.Equals(formatName, StringComparison.InvariantCultureIgnoreCase));

        return writer.Write(mediateImage);
    }
}