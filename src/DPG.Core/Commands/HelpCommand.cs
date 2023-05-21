using System.Text;

namespace DPG.Core.Commands;

internal static class HelpCommand
{
    public const string Name = "help";

    public const string Alias = "-h";

    public static void Execute()
    {
        var helpMessageBuilder = new StringBuilder();

        helpMessageBuilder.AppendLine("dpg <command>");
        helpMessageBuilder.AppendLine()
            .AppendLine("Usage")
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
