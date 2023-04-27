using System.Reflection;
using Converter.Abstractions;
using Lab2.Plugins.Abstractions;

namespace Lab2.Plugins;

public sealed class PluginLoader : IPluginLoader
{
    private static readonly DirectoryInfo PluginsDirectory = new("Plugins");

    public IImageWriter[] LoadWriters()
    {
        var writerFiles = PluginsDirectory.EnumerateFiles("Writer.*.dll");

        return writerFiles
            .Select(f =>
            {
                var assembly = Assembly.LoadFile(f.FullName);

                var readerType = assembly
                    .GetTypes()
                    .FirstOrDefault(t => t.GetInterface(nameof(IImageWriter)) is not null);

                if (readerType is null)
                    return null;

                return (IImageWriter) Activator.CreateInstance(readerType)!;
            })
            .Where(r => r is not null)
            .ToArray()!;
    }

    public IImageReader[] LoadReaders()
    {
        var readerFiles = PluginsDirectory.EnumerateFiles("Reader.*.dll");

        return readerFiles
            .Select(f =>
            {
                var assembly = Assembly.LoadFile(f.FullName);

                var readerType = assembly
                    .GetTypes()
                    .FirstOrDefault(t => t.GetInterface(nameof(IImageReader)) is not null);

                if (readerType is null)
                    return null;

                return (IImageReader) Activator.CreateInstance(readerType)!;
            })
            .Where(r => r is not null)
            .ToArray()!;
    }
}