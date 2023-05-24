namespace Lab3.Configuration.Abstractions;

public interface IConfigValidator
{
    void Validate(Config config);
}