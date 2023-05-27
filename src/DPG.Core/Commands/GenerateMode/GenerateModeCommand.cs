using System.Reflection;
using System.Text;

namespace DPG.Core.Commands.GenerateMode;

internal static class GenerateModeCommand
{
    public const string Name = "mode";

    public const string Alias = "-m";

    public const string Description = "set mode for password generator";

    public const string HelpMessage = "Set the password generator mode to be used.";

    public static void Execute(string[] optionInput)
    {
        Console.WriteLine("Executing the generate mode command.");

        if (optionInput.Contains(HelpCommand.Name) ||
            optionInput.Contains(HelpCommand.Alias) ||
            optionInput.Length == 1)
        {
            ShowHelpMessage();
        }

    }

    private static void ShowHelpMessage()
    {
        var helpMessage = BuildMessageContent();

        Console.WriteLine(helpMessage);
    }

    private static string BuildMessageContent()
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

    private static string GetGenerateModeOptionsContent()
    {
        var optionsMessageBuilder = new StringBuilder();
        var options = GetOptions();
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

    private static IEnumerable<Type>? GetOptions()
    {
        var optionsNamespace = "DPG.Core.Commands.GenerateMode.Options";
        var options = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Namespace == optionsNamespace
                && t.IsSealed && t.IsAbstract)
            .ToList();

        return options;
    }
}
