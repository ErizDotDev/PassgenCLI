namespace DPG.Core.Commands;

internal static class GenerateModeCommand
{
    public const string Name = "mode";

    public const string Alias = "-m";

    public const string Description = "set mode for password generator";

    public const string HelpMessage = "help message for mode";

    public static void Execute(string[] optionInput)
    {
        Console.WriteLine("Executing the generate mode command.");

        if (optionInput.Contains(HelpCommand.Name) ||
            optionInput.Contains(HelpCommand.Alias) ||
            optionInput.Length == 1)
            ShowHelpMessage();
    }

    private static void ShowHelpMessage()
    {
        Console.WriteLine(HelpMessage);
    }
}
