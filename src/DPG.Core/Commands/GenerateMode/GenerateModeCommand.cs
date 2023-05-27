using System.Reflection;
using System.Text;

namespace DPG.Core.Commands.GenerateMode;

internal class GenerateModeCommand : BaseCommand
{
    public const string Name = "mode";

    public const string Alias = "-m";

    public const string Description = "set mode for password generator";

    public const string HelpMessage = "Set the password generator mode to be used.";

    private readonly IEnumerable<Type>? options;

    public GenerateModeCommand()
    {
        options = GetOptions();
    }

    public override void Execute(string[] optionInput)
    {
        if (optionInput.Contains(HelpCommand.Name) ||
            optionInput.Contains(HelpCommand.Alias))
        {
            ShowHelpMessage();
            return;
        }

        try
        {
            var mode = optionInput[0];

            if (!ModeOptionExists(mode))
            {
                Console.WriteLine("Error: provided mode option not found!");
                ShowHelpMessage();
            }

            Console.WriteLine("Successfully extracted command options for generate mode.");
        }
        catch (IndexOutOfRangeException)
        {
            ShowHelpMessage();
        }
        catch
        {
            Console.WriteLine("Something went wrong with the application!");
        }
    }

    public override void ShowHelpMessage()
    {
        var helpMessage = BuildMessageContent();

        Console.WriteLine(helpMessage);
    }

    private string BuildMessageContent()
    {
        var modeHelpMessageBuilder = new StringBuilder();

        modeHelpMessageBuilder.AppendLine()
            .AppendLine("usage: dpg mode [options] [arguments]")
            .AppendLine()
            .AppendLine(HelpMessage)
            .AppendLine();

        var generateModeOptions = GetGenerateModeOptionsContent();

        modeHelpMessageBuilder.AppendLine("Options:")
            .AppendLine()
            .AppendLine(generateModeOptions);

        return modeHelpMessageBuilder.ToString();
    }

    private string GetGenerateModeOptionsContent()
    {
        var optionsMessageBuilder = new StringBuilder();
        var targetPropName = "Name";

        foreach (var option in options!)
        {
            foreach (var propInfo in option.GetFields())
            {
                if (propInfo.Name != targetPropName)
                    continue;

                var propValue = propInfo.GetRawConstantValue();
                optionsMessageBuilder.AppendLine($"\t{propValue}");
            }
        }

        return optionsMessageBuilder.ToString();
    }

    private IEnumerable<Type>? GetOptions()
    {
        var optionsNamespace = "DPG.Core.Commands.GenerateMode.Options";
        var options = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Namespace == optionsNamespace
                && t.IsSealed && t.IsAbstract)
            .ToList();

        return options;
    }

    private bool ModeOptionExists(string providedOption)
    {
        bool isExists = false;
        var targetPropName = "Name";

        foreach (var option in options!)
        {
            foreach (var propInfo in option.GetFields())
            {
                if (propInfo.Name != targetPropName)
                    continue;

                var propValue = propInfo.GetRawConstantValue() as string;
                if (propValue == providedOption)
                    return true;
            }
        }

        return isExists;
    }
}
