namespace DPG.Core;

public class PasswordGenerator
{
    private string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private string NumericChars = "01234567890";
    private string SpecialChars = "!\"#$%&\'+,./:;=?@\\^|~";

    private char[]? UppercaseCharsArr;
    private char[]? LowercaseCharsArr;
    private char[]? NumericCharsArr;
    private char[]? SpecialCharsArr;

    public PasswordGenerator()
    {
        InitializeCharacterArrays();
    }

    public string GeneratePassword(PasswordGeneratorStandardOptions options)
    {
        var password = string.Empty;

        var availableCharacters = GetCharacterPool(options);

        if (availableCharacters.Count <= 0)
        {
            throw new Exception("No char array selected.");
        }

        var characterPoolArrLength = availableCharacters.Count;
        var randomizer = new Random();

        for (int i = 1; i <= options.PasswordLength; i++)
        {
            var randomCharPoolIndex = randomizer.Next(0, characterPoolArrLength - 1);
            var randomizedPool = availableCharacters[randomCharPoolIndex];

            var randomCharIndex = randomizer.Next(0, randomizedPool.Length - 1);
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
