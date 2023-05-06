namespace DPG.Core;

public interface IPasswordGeneratorOptions { }

public class PasswordGeneratorStandardOptions : IPasswordGeneratorOptions
{
    public int PasswordLength { get; set; } = 16;

    public bool EnableUppercaseCharacters { get; set; } = true;

    public bool EnableLowercaseCharacters { get; set; } = true;

    public bool EnableNumericCharacters { get; set; } = true;

    public bool EnableSpecialCharacters { get; set; } = true;
}

public class PasswordGeneratorEncodeOptions : IPasswordGeneratorOptions
{
    public string PassPhrase { get; set; } = string.Empty;

    public bool SubstituteSpacesWithUnderscores { get; set; } = false;
}
