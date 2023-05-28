using System.Text;

namespace DPG.Core.Commands.GenerateMode.Options;

internal class StandardGenerateModeOption : IGenerateModeOption
{
    public const string Name = "standard";

    public const string Description = "generates a random password";

    private Dictionary<string, string> Flags = new Dictionary<string, string>();

    private const int DEFAULT_PASSWORD_LENGTH = 16;

    public StandardGenerateModeOption()
    {
        Flags = LoadFlags();
    }

    public void Execute(string[] arguments)
    {
        if (arguments.Contains(HelpCommand.Alias))
            ShowHelpMessage();

        try
        {
            var passwordLength = 0;
            var passwordGenerator = new StandardPasswordGenerator() as IPasswordGenerator;

            if (arguments.Contains(Flags["PasswordLength"]))
            {
                var passwordLengthArgIndex = Array.IndexOf(arguments, Flags["PasswordLength"]);
                passwordLength = GetPasswordLength(arguments, passwordLengthArgIndex);
            }
            else
            {
                passwordLength = DEFAULT_PASSWORD_LENGTH;
            }

            if (passwordLength < 1)
            {
                Console.WriteLine("Error: Invalid password length provided");
                return;
            }

            var standardModeOptions = new PasswordGeneratorStandardOptions
            {
                PasswordLength = passwordLength,
                EnableUppercaseCharacters = !arguments.Contains(Flags["DisableUppercase"]),
                EnableLowercaseCharacters = !arguments.Contains(Flags["DisableLowercase"]),
                EnableNumericCharacters = !arguments.Contains(Flags["DisableNumerals"]),
                EnableSpecialCharacters = !arguments.Contains(Flags["DisableSpecialChars"]),
            };

            var generatedPassword = passwordGenerator.GeneratePassword(standardModeOptions);

            Console.WriteLine($"Generated password is:\n{generatedPassword}");
        }
        catch
        {
            Console.WriteLine("Something went wrong with the application!");
        }
    }

    private void ShowHelpMessage()
    {
        var helpMessage = BuildMessageContent();
        Console.WriteLine(helpMessage);
    }

    private string BuildMessageContent()
    {
        var helpMessageBuilder = new StringBuilder();
        var flagsContent = GetAvailableFlagsForDisplay();

        helpMessageBuilder.AppendLine()
            .AppendLine("usage: dpg mode standard [arguments]")
            .AppendLine()
            .AppendLine("Available options:\n")
            .AppendLine(flagsContent);

        return helpMessageBuilder.ToString();
    }

    private Dictionary<string, string> LoadFlags()
    {
        return new Dictionary<string, string>()
        {
            { "PasswordLength", "--password-length" },
            { "DisableUppercase", "--disable-uppercase-chars" },
            { "DisableLowercase", "--disable-lowercase-chars" },
            { "DisableNumerals", "--disable-numeric-chars" },
            { "DisableSpecialChars", "--disable-special-chars" }
        };
    }

    private int GetPasswordLength(string[] arguments, int passwordLengthOptionIndex)
    {
        Console.WriteLine(passwordLengthOptionIndex);

        var passwordLength = DEFAULT_PASSWORD_LENGTH;

        if (!int.TryParse(arguments[passwordLengthOptionIndex + 1], out passwordLength))
        {
            return -1;
        }

        return passwordLength;
    }

    private string GetAvailableFlagsForDisplay()
    {
        var flagsMessageBuilder = new StringBuilder();

        foreach (var flag in Flags)
        {
            Console.WriteLine($"  {flag.Value}");
        }

        return flagsMessageBuilder.ToString();
    }
}
