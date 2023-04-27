namespace DPG.Core;

public class EncodePasswordGenerator : IPasswordGenerator
{
    private readonly Dictionary<char, char[]> characterMap;

    public EncodePasswordGenerator()
    {
        characterMap = GetCharacterMap();
    }

    public string GeneratePassword(IPasswordGeneratorOptions options)
    {
        var encodeOptions = options! as PasswordGeneratorEncodeOptions;
        var passPhraseCharArray = encodeOptions?.PassPhrase.ToCharArray();

        return string.Empty;
    }

    private Dictionary<char, char[]> GetCharacterMap()
    {
        return new Dictionary<char, char[]>()
        {
            { 'a', new char[] { 'a', 'A', '4', '@' } },
            { 'b', new char[] { 'b', 'B', '6', '%' } },
            { 'c', new char[] { 'c', 'C', '{', '(' } },
            { 'd', new char[] { 'd', 'D' } },
            { 'e', new char[] { 'e', 'E', '3' } },
            { 'f', new char[] { 'f', 'F' } },
            { 'g', new char[] { 'g', 'G' } },
            { 'h', new char[] { 'h', 'H', '#' } },
            { 'i', new char[] { 'i', 'I', '1', '!' } },
            { 'j', new char[] { 'j', 'J' } },
            { 'k', new char[] { 'k', 'K', '<' } },
            { 'l', new char[] { 'l', 'L', '|', '/' } },
            { 'm', new char[] { 'm', 'M' } },
            { 'n', new char[] { 'n', 'N' } },
            { 'o', new char[] { 'o', 'O', '0' } },
            { 'p', new char[] { 'p', 'P' } },
            { 'q', new char[] { 'q', 'Q', '9' } },
            { 'r', new char[] { 'r', 'R' } },
            { 's', new char[] { 's', 'S', '5', '$' } },
            { 't', new char[] { 't', 'T', '7' } },
            { 'u', new char[] { 'u', 'U' } },
            { 'v', new char[] { 'v', 'V', '>' } },
            { 'w', new char[] { 'w', 'W' } },
            { 'x', new char[] { 'x', 'X', '*' } },
            { 'y', new char[] { 'y', 'Y', '?' } },
            { 'z', new char[] { 'z', 'Z' } },
        };
    }
}
