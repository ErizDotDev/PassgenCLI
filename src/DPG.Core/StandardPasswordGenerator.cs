namespace DPG.Core;

public class StandardPasswordGenerator : IPasswordGenerator
{
    private string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private string NumericChars = "01234567890";
    private string SpecialChars = "!\"#$%&\'+,./:;=?@\\^|~";

    private char[]? UppercaseCharsArr;
    private char[]? LowercaseCharsArr;
    private char[]? NumericCharsArr;
    private char[]? SpecialCharsArr;

    public StandardPasswordGenerator()
    {
        InitializeCharacterArrays();
    }

    public string GeneratePassword(IPasswordGeneratorOptions _options)
    {
        var standardOptions = _options! as PasswordGeneratorStandardOptions;

        if (standardOptions!.PasswordLength <= 0)
        {
            throw new Exception("Please provide password length value greater than 0.");
        }

        var password = string.Empty;

        var availableCharacters = GetCharacterPool(standardOptions);
        if (availableCharacters.Count <= 0)
        {
            throw new Exception("No char array selected.");
        }

        var characterPoolArrLength = availableCharacters.Count;
        var randomizer = new Random();

        for (int i = 1; i <= standardOptions.PasswordLength; i++)
        {
            var randomCharPoolIndex = randomizer.Next(0, characterPoolArrLength);
            var randomizedPool = availableCharacters[randomCharPoolIndex];

            var randomCharIndex = randomizer.Next(0, randomizedPool.Length);
            var randomChar = randomizedPool[randomCharIndex];

            password += randomChar;
        }

        return password;
    }

    private void InitializeCharacterArrays()
    {
        UppercaseCharsArr = UppercaseChars.ToCharArray();
        LowercaseCharsArr = LowercaseChars.ToCharArray();
        NumericCharsArr = NumericChars.ToCharArray();
        SpecialCharsArr = SpecialChars.ToCharArray();
    }

    private List<char[]> GetCharacterPool(PasswordGeneratorStandardOptions options)
    {
        var availableChars = new List<char[]>();

        if (options.EnableUppercaseCharacters)
        {
            availableChars.Add(UppercaseCharsArr!);
        }

        if (options.EnableLowercaseCharacters)
        {
            availableChars.Add(LowercaseCharsArr!);
        }

        if (options.EnableSpecialCharacters)
        {
            availableChars.Add(SpecialCharsArr!);
        }

        if (options.EnableNumericCharacters)
        {
            availableChars.Add(NumericCharsArr!);
        }

        return availableChars;
    }
}
