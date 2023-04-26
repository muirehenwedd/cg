namespace Lab2.Configuration;

public sealed class Config
{
    public required string InputFilePath { get; init; }
    public required string GoalFormatName { get; init; }
    public required string? OutputFilePath { get; init; }
}