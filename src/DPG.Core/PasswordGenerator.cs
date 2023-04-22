using System.Text;

namespace DPG.Core;

public class PasswordGenerator
{
    private string[] commandLineArgs;

    public PasswordGenerator(string[] args)
    {
        commandLineArgs = args;
    }

    public string Generate()
    {
        if (commandLineArgs.Length == 0)
        {
            return ShowHelpMessage();
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
}