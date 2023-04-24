namespace DPG.Core;

public class PasswordGeneratorStandardOptions
{
    public int PasswordLength { get; set; } = 16;

    public bool EnableUppercaseCharacters { get; set; } = true;

    public bool EnableLowercaseCharacters { get; set; } = true;

    public bool EnableNumericCharacters { get; set; } = true;

    public bool EnableSpecialCharacters { get; set; } = true;
}
