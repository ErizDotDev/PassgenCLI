namespace DPG.Core.Commands;

internal static class HelpCommand
{
    public const string Name = "help";

    public const string Alias = "-h";

    public static void Execute()
    {
        Console.WriteLine("Hello Help!");
    }
}
