using Lab2.Configuration.Abstractions;
using Lab2.Plugins.Abstractions;

namespace Lab2.Configuration;

public sealed class ConfigValidator : IConfigValidator
{
    private readonly IPluginManager _pluginManager;

    public ConfigValidator(IPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
    }

    public void Validate(Config config)
    {
        var inputFileInfo = new FileInfo(config.InputFilePath);

        if (!inputFileInfo.Exists)
            throw new Exception("Input file dont exists.");

        if (!_pluginManager.WriterPluginExists(config.GoalFormatName))
            throw new Exception("There is no plugin for this output format.");

        if (config.OutputFilePath is not null)
        {
            if (!new FileInfo(config.OutputFilePath).Directory!.Exists)
                throw new Exception("Output folder dont exists.");
        }
    }
}