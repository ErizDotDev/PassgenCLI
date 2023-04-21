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
         Console.WriteLine("dpg <command>");
      }

      return string.Empty;
   }
}