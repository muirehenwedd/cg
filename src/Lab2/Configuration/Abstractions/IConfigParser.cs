namespace Lab2.Configuration.Abstractions;

public interface IConfigParser
{
    Config Parse(string[] args);
}