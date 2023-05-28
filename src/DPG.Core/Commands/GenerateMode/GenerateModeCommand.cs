using DPG.Core.Commands.GenerateMode.Options;
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
            var (isModeExists, optionType) = TryGetOptionType(mode);

            if (!isModeExists)
            {
                Console.WriteLine("Error: provided mode option not found!");
                ShowHelpMessage();
            }

            var modeOption = Activator.CreateInstance(optionType) as IGenerateModeOption;
            var commandArgs = optionInput.Skip(1).ToArray();
            modeOption!.Execute(commandArgs);
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
        var targetPropNames = new string[] { "Name", "Description" };

        foreach (var option in options!)
        {
            foreach (var propInfo in option.GetFields())
            {
                if (!targetPropNames.Contains(propInfo.Name))
                    continue;

                var propValue = propInfo.GetRawConstantValue();

                if (targetPropNames[0] == propInfo.Name)
                    optionsMessageBuilder.Append($"  {propValue}");

                if (targetPropNames[1] == propInfo.Name)
                    optionsMessageBuilder.AppendLine($"\t\t{propValue}");
            }
        }

        return optionsMessageBuilder.ToString();
    }

    private IEnumerable<Type>? GetOptions()
    {
        var optionsNamespace = "DPG.Core.Commands.GenerateMode.Options";
        var options = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Namespace == optionsNamespace)
            .ToList();

        return options;
    }

    private (bool, Type) TryGetOptionType(string providedOption)
    {
        bool isExists = false;
        var targetPropName = "Name";
        Type optionType = null!;

        foreach (var option in options!)
        {
            foreach (var propInfo in option.GetFields())
            {
                if (propInfo.Name != targetPropName)
                    continue;

                var propValue = propInfo.GetRawConstantValue() as string;
                if (propValue == providedOption)
                {
                    isExists = true;
                    optionType = option;
                    break;
                }
            }
        }

        return (isExists, optionType);
    }
}
