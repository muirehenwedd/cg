using Converter.Abstractions;

namespace Lab2.Plugins.Abstractions;

public interface IPluginLoader
{
    IImageWriter[] LoadWriters();
    IImageReader[] LoadReaders();
}