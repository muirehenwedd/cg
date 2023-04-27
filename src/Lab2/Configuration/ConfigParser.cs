using Lab2.Configuration.Abstractions;

namespace Lab2.Configuration;

public sealed class ConfigParser : IConfigParser
{
    public Config Parse(string[] args)
    {
        const string inputFileArgName = "--source=";
        const string goalFormatArgName = "--goal-format=";
        const string outputFilePathArgName = "--output=";

        var inputFileArg = args.FirstOrDefault(str => str.StartsWith(inputFileArgName));

        if (inputFileArg is null)
            throw new ArgumentException("Missing required argument", inputFileArgName);

        var goalFormatArg = args.FirstOrDefault(str => str.StartsWith(goalFormatArgName));

        if (goalFormatArg is null)
            throw new ArgumentException("Missing required argument", goalFormatArgName);

        var outputFilePathArg = args.FirstOrDefault(str => str.StartsWith(outputFilePathArgName));

        var inputFileArgValue = inputFileArg[inputFileArgName.Length..];
        var goalFormatArgValue = goalFormatArg[goalFormatArgName.Length..];
        var outputFilePathArgValue = outputFilePathArg?[outputFilePathArgName.Length..];

        return new Config
        {
            InputFilePath = inputFileArgValue,
            GoalFormatName = goalFormatArgValue,
            OutputFilePath = outputFilePathArgValue
        };
    }
}