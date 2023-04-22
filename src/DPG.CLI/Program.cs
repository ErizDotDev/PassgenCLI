using DPG.Core;

namespace DPG.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        var passwordGenerator = new PasswordGenerator(args);
        var result = passwordGenerator.Generate();

        Console.WriteLine(result);
    }
}