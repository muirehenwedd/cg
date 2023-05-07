using Lab3.Configuration.Abstractions;

namespace Lab3.Configuration;

public sealed class ConfigParser : IConfigParser
{
    public Config Parse(string[] args)
    {
        const string inputFileArgName = "--source=";
        const string outputFilePathArgName = "--output=";

        var inputFileArg = args.FirstOrDefault(str => str.StartsWith(inputFileArgName));

        if (inputFileArg is null)
            throw new ArgumentException("Missing required argument", inputFileArgName);

        var outputFilePathArg = args.FirstOrDefault(str => str.StartsWith(outputFilePathArgName));

        if (outputFilePathArg is null)
            throw new ArgumentException("Missing required argument", outputFilePathArgName);

        var inputFileArgValue = inputFileArg[inputFileArgName.Length..];
        var outputFilePathArgValue = outputFilePathArg[outputFilePathArgName.Length..];

        return new Config
        {
            InputFilePath = inputFileArgValue,
            OutputFilePath = outputFilePathArgValue
        };
    }
}