using DPG.Core;

namespace DPG.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        var passwordGeneratorDriver = new PasswordGeneratorDriver(args);
        var result = passwordGeneratorDriver.Execute();

        Console.WriteLine(result);
    }
}