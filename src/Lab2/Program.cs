using Lab2.Configuration;
using Lab2.Configuration.Abstractions;
using Lab2.Plugins;
using Lab2.Plugins.Abstractions;

internal class Program
{
    public static void Main(string[] args)
    {
        IConfigParser configParser = new ConfigParser();
        var config = configParser.Parse(args);

        IPluginLoader pluginLoader = new PluginLoader();
        IPluginManager pluginManager = new PluginManager(pluginLoader);
        IConfigValidator configValidator = new ConfigValidator(pluginManager);
        configValidator.Validate(config);

        var bytes = File.ReadAllBytes(config.InputFilePath);

        if (!pluginManager.TryReadImage(bytes, out var mediateImage))
        {
            Console.WriteLine("Reader not found");
            Thread.Sleep(TimeSpan.FromSeconds(2));

            return;
        }

        var outputBytes = pluginManager.WriteImage(mediateImage!, config.GoalFormatName);

        var outputPath = config.OutputFilePath ?? config.InputFilePath[..config.InputFilePath.LastIndexOf('.')] + '.' +
            config.GoalFormatName.ToLower();

        using var fileStream = new FileStream(outputPath, FileMode.OpenOrCreate);

        fileStream.Write(outputBytes);
    }
}