using Lab3.Configuration.Abstractions;

namespace Lab3.Configuration;

public sealed class ConfigValidator : IConfigValidator
{
    public void Validate(Config config)
    {
        var inputFileInfo = new FileInfo(config.InputFilePath);

        if (!inputFileInfo.Exists)
            throw new Exception("Input file dont exists.");

        if (!new FileInfo(config.OutputFilePath).Directory!.Exists)
            throw new Exception("Output folder dont exists.");
    }
}