using Converter.Abstractions;

namespace Lab3.Plugins.Abstractions;

public interface IPluginLoader
{
    IImageWriter[] LoadWriters();
}