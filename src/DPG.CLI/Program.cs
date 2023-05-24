using DPG.Core;

namespace DPG.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        var passwordGeneratorDriver = new PasswordGeneratorDriver(args);
        passwordGeneratorDriver.Execute();
    }
}