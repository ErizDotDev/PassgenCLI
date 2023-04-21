using DPG.Core;

namespace DPG.CLI;

internal class Program
{
   static void Main(string[] args)
   {
      var passwordGenerator = new PasswordGenerator(args);

      if (args.Length == 0)
      {
         Console.WriteLine("dpg <command>");
      }
   }
}