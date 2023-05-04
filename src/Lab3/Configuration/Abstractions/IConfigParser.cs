namespace Lab3.Configuration.Abstractions;

public interface IConfigParser
{
    Config Parse(string[] args);
}