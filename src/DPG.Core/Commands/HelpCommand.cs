using DPG.Core.Commands.GenerateMode;
using System.Text;

namespace DPG.Core.Commands;

internal class HelpCommand : BaseCommand
{
    public const string Name = "help";

    public const string Alias = "-h";

    public override void ShowHelpMessage()
    {
        var helpMessageBuilder = new StringBuilder();

        helpMessageBuilder.AppendLine("usage: dpg <command>");
        helpMessageBuilder.AppendLine()
            .AppendLine("Usage:")
            .AppendLine()
            .AppendLine(GetMainCommandDescriptions());

        Console.WriteLine(helpMessageBuilder.ToString());
    }

    private static string GetMainCommandDescriptions()
    {
        var descriptionBuilder = new StringBuilder();

        descriptionBuilder
            .Append($"dpg {GenerateModeCommand.Name}\t{GenerateModeCommand.Description}");

        return descriptionBuilder.ToString();
    }
}
