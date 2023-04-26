namespace DPG.Core;

public interface IPasswordGenerator
{
    string GeneratePassword(IPasswordGeneratorOptions options);
}
