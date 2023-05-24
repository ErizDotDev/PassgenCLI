using DPG.Core.Commands;
using System.Text;

namespace DPG.Core;

public class PasswordGeneratorDriver
{
    private string[] commandLineArgs;
    private IPasswordGenerator? passwordGenerator;

    private const string STANDARD_MODE = "standard";
    private const string ENCODE_MODE = "encode";

    private const string PASSWORD_LENGTH_OPTION = "--password-length";
    private const string DISABLE_UPPERCASE_CHARS_OPTION = "--disable-uppercase-chars";
    private const string DISABLE_LOWERCASE_CHARS_OPTION = "--disable-lowercase-chars";
    private const string DISABLE_NUMERIC_CHARS_OPTION = "--disable-numeric-chars";
    private const string DISABLE_SPECIAL_CHARS_OPTION = "--disable-special-chars";

    private const string SUB_SPACES_WITH_UNDERSCORE = "--substitute-whitespace-with-underscore";

    private static readonly int DEFAULT_PASSWORD_LENGTH = 16;

    public PasswordGeneratorDriver(string[] args)
    {
        commandLineArgs = args;
    }

    public void Execute()
    {
        if (commandLineArgs.Length == 0)
            HelpCommand.Execute();

        switch (commandLineArgs[0])
        {
            case HelpCommand.Name:
            case HelpCommand.Alias:
                if (commandLineArgs.Length == 1)
                    HelpCommand.Execute();

                goto case GenerateModeCommand.Alias;

            case GenerateModeCommand.Name:
            case GenerateModeCommand.Alias:
                GenerateModeCommand.Execute();
                break;
        }
    }

    public string GeneratePasswordViaSelectedMode()
    {
        if (commandLineArgs.Contains(HelpCommand.Name) ||
            commandLineArgs.Contains(HelpCommand.Alias) ||
            commandLineArgs.Length == 1)
        {
            return ShowModeHelpMessage();
        }

        var mode = commandLineArgs[1];

        if (mode != STANDARD_MODE && mode != ENCODE_MODE)
        {
            return ShowModeHelpMessage();
        }

        switch (mode)
        {
            case STANDARD_MODE:
                return GeneratePasswordViaStandardMode();
            case ENCODE_MODE:
                return GeneratePasswordViaEncodeMode();
            default:
                throw new Exception("Incorrect mode specified.");
        }
    }

    private string ShowModeHelpMessage()
    {
        var modeHelpMessageBuilder = new StringBuilder();

        modeHelpMessageBuilder.AppendLine("Set the password generator mode to be used.");
        modeHelpMessageBuilder.AppendLine()
            .AppendLine("Usage")
            .AppendLine("dpg mode [standard | encode] [options]")
            .AppendLine();
        modeHelpMessageBuilder.AppendLine("Options:")
            .AppendLine("[--password-length <password length>]")
            .AppendLine("[--disable-uppercase-chars]")
            .AppendLine("[--disable-lowercase-chars]")
            .AppendLine("[--disable-numeric-chars]")
            .AppendLine("[--disable-special-chars]");

        return modeHelpMessageBuilder.ToString();
    }

    private int GetPasswordLength(int passwordLengthOptionIndex)
    {
        Console.WriteLine(passwordLengthOptionIndex);

        var passwordLength = DEFAULT_PASSWORD_LENGTH;

        if (!int.TryParse(commandLineArgs[passwordLengthOptionIndex + 1], out passwordLength))
        {
            return -1;
        }

        return passwordLength;
    }

    private string GeneratePasswordViaStandardMode()
    {
        if (commandLineArgs.Contains(HelpCommand.Alias))
        {
            return ShowStandardModeHelpMessage();
        }

        var passwordLength = 0;

        passwordGenerator = new StandardPasswordGenerator();

        if (commandLineArgs.Contains(PASSWORD_LENGTH_OPTION))
        {
            var passwordLengthOptionIndex = Array.IndexOf(commandLineArgs, PASSWORD_LENGTH_OPTION);
            passwordLength = GetPasswordLength(passwordLengthOptionIndex);
        }
        else
        {
            passwordLength = DEFAULT_PASSWORD_LENGTH;
        }

        if (passwordLength < 1)
        {
            throw new Exception("Invalid password length provided.");
        }

        var standardModeOptions = new PasswordGeneratorStandardOptions
        {
            PasswordLength = passwordLength,
            EnableUppercaseCharacters = !commandLineArgs.Contains(DISABLE_UPPERCASE_CHARS_OPTION),
            EnableLowercaseCharacters = !commandLineArgs.Contains(DISABLE_LOWERCASE_CHARS_OPTION),
            EnableNumericCharacters = !commandLineArgs.Contains(DISABLE_NUMERIC_CHARS_OPTION),
            EnableSpecialCharacters = !commandLineArgs.Contains(DISABLE_SPECIAL_CHARS_OPTION)
        };

        var generatedPassword = passwordGenerator.GeneratePassword(standardModeOptions);

        return generatedPassword;
    }

    private string GeneratePasswordViaEncodeMode()
    {
        if (commandLineArgs.Contains(HelpCommand.Alias))
        {
            return ShowEncodeModeHelpMessage();
        }

        passwordGenerator = new EncodePasswordGenerator();

        var passPhrase = commandLineArgs[2];
        if (passPhrase is null || string.IsNullOrEmpty(passPhrase))
        {
            throw new Exception("Please provide a valid pass phrase.");
        }

        var encodeModeOptions = new PasswordGeneratorEncodeOptions
        {
            PassPhrase = passPhrase,
            SubstituteSpacesWithUnderscores = commandLineArgs.Contains(SUB_SPACES_WITH_UNDERSCORE)
        };

        var generatedPassword = passwordGenerator.GeneratePassword(encodeModeOptions);

        return generatedPassword;
    }

    private string ShowStandardModeHelpMessage()
    {
        var standardModeHelpMessageBuilder = new StringBuilder();

        standardModeHelpMessageBuilder.AppendLine("Usage")
            .AppendLine("dpg mode standard [...options]");

        standardModeHelpMessageBuilder.AppendLine("Options:")
            .AppendLine("[--password-length <password length>]")
            .AppendLine("[--disable-uppercase-chars]")
            .AppendLine("[--disable-lowercase-chars]")
            .AppendLine("[--disable-numeric-chars]")
            .AppendLine("[--disable-special-chars]");

        return standardModeHelpMessageBuilder.ToString();
    }


    private string ShowEncodeModeHelpMessage()
    {
        var standardModeHelpMessageBuilder = new StringBuilder();

        standardModeHelpMessageBuilder.AppendLine("Usage")
            .AppendLine("dpg mode encode [...options]");

        standardModeHelpMessageBuilder.AppendLine("Options:")
            .AppendLine("[--substitute-whitespace-with-underscore]");

        return standardModeHelpMessageBuilder.ToString();
    }
}
