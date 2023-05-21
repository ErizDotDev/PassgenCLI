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
            .AppendLine("Usage");

        Console.WriteLine(helpMessageBuilder.ToString());
    }
}
