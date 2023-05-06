using System.Text;

namespace DPG.Core;

public class PasswordGeneratorDriver
{
    private string[] commandLineArgs;
    private IPasswordGenerator? passwordGenerator;

    private const string HELP_ALIAS = "-h";
    private const string HELP_COMMAND_TEXT = "help";
    private const string MODE_ALIAS = "-m";
    private const string MODE_COMMAND_TEXT = "mode";

    private const string STANDARD_MODE = "standard";
    private const string ENCODE_MODE = "encode";

    private const string PASSWORD_LENGTH_OPTION = "--password-length";
    private const string DISABLE_UPPERCASE_CHARS_OPTION = "--disable-uppercase-chars";
    private const string DISABLE_LOWERCASE_CHARS_OPTION = "--disable-lowercase-chars";
    private const string DISABLE_NUMERIC_CHARS_OPTION = "--disable-numeric-chars";
    private const string DISABLE_SPECIAL_CHARS_OPTION = "--disable-special-chars";

    private const int DEFAULT_PASSWORD_LENGTH = 16;

    public PasswordGeneratorDriver(string[] args)
    {
        commandLineArgs = args;
    }

    public string Execute()
    {
        if (commandLineArgs.Length == 0)
        {
            return ShowHelpMessage();
        }

        switch (commandLineArgs[0])
        {
            case HELP_ALIAS:
            case HELP_COMMAND_TEXT:
                if (commandLineArgs.Length == 1)
                {
                    return ShowHelpMessage();
                }

                goto case MODE_ALIAS;

            case MODE_ALIAS:
            case MODE_COMMAND_TEXT:
                return GeneratePasswordViaSelectedMode();
        }

        return string.Empty;
    }

    private string ShowHelpMessage()
    {
        var helpMessageBuilder = new StringBuilder();

        helpMessageBuilder.AppendLine("dpg <command>");
        helpMessageBuilder.AppendLine()
            .AppendLine("Usage");
        helpMessageBuilder.AppendLine()
            .AppendLine("dpg mode\tset password generation mode");

        return helpMessageBuilder.ToString();
    }

    public string GeneratePasswordViaSelectedMode()
    {
        if (commandLineArgs.Contains(HELP_ALIAS) ||
            commandLineArgs.Contains(HELP_COMMAND_TEXT) ||
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
        modeHelpMessageBuilder.AppendLine("Options: (Note: Options below are only placeholder values)")
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
        passwordGenerator = new EncodePasswordGenerator();

        var passPhrase = commandLineArgs[2];
        if (passPhrase is null || string.IsNullOrEmpty(passPhrase))
        {
            throw new Exception("Please provide a valid pass phrase.");
        }

        var encodeModeOptions = new PasswordGeneratorEncodeOptions
        {
            PassPhrase = passPhrase,
        };

        var generatedPassword = passwordGenerator.GeneratePassword(encodeModeOptions);

        return generatedPassword;
    }
}