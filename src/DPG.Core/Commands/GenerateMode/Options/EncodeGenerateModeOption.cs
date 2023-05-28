using System.Text;

namespace DPG.Core.Commands.GenerateMode.Options;

internal class EncodeGenerateModeOption : IGenerateModeOption
{
    public const string Name = "encode";

    public const string Description = "encodes a provided pass phrase with password safe characters";

    private Dictionary<string, string> Flags = new Dictionary<string, string>();

    public EncodeGenerateModeOption()
    {
        Flags = LoadFlags();
    }

    public void Execute(string[] arguments)
    {
        if (arguments.Contains(HelpCommand.Alias))
            ShowHelpMessage();

        try
        {
            var passwordGenerator = new EncodePasswordGenerator() as IPasswordGenerator;

            var passPhrase = arguments[0];
            if (string.IsNullOrEmpty(passPhrase))
            {
                Console.WriteLine("Provide a valid pass phrase");
                return;
            }

            var encodeModeOptions = new PasswordGeneratorEncodeOptions()
            {
                PassPhrase = passPhrase,
                SubstituteSpacesWithUnderscores = arguments.Contains(Flags["SubSpaces"])
            };

            var generatedPassword = passwordGenerator.GeneratePassword(encodeModeOptions);

            Console.WriteLine($"Generated password is:\n{generatedPassword}");
        }
        catch (IndexOutOfRangeException)
        {
            ShowHelpMessage();
        }
        catch
        {
            Console.WriteLine("Something went wrong with the application.");
        }
    }

    private Dictionary<string, string> LoadFlags()
    {
        return new Dictionary<string, string>()
        {
            { "SubSpaces", "--substitute-whitespace-with-underscore" }
        };
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
            .AppendLine("usage: dpg mode standard [pass phrase] [arguments]")
            .AppendLine()
            .AppendLine("Available options:\n")
            .AppendLine(flagsContent);

        return helpMessageBuilder.ToString();
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
