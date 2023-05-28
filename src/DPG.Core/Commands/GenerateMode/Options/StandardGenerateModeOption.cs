using System.Text;

namespace DPG.Core.Commands.GenerateMode.Options;

internal class StandardGenerateModeOption : IGenerateModeOption
{
    public const string Name = "standard";

    public const string Description = "generates a random password";

    public void Execute(string[] arguments)
    {
        if (arguments.Contains(HelpCommand.Alias))
            ShowHelpMessage();
    }

    private void ShowHelpMessage()
    {
        var helpMessage = BuildMessageContent();

        Console.WriteLine(helpMessage);
    }

    private string BuildMessageContent()
    {
        var helpMessageBuilder = new StringBuilder();

        helpMessageBuilder.AppendLine()
            .AppendLine("usage: dpg mode standard [arguments]")
            .AppendLine();

        return helpMessageBuilder.ToString();
    }
}
