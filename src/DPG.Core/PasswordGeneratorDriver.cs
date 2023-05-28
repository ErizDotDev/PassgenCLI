using DPG.Core.Commands;
using DPG.Core.Commands.GenerateMode;

namespace DPG.Core;

public class PasswordGeneratorDriver
{
    private string[] commandLineArgs;

    public PasswordGeneratorDriver(string[] args)
    {
        commandLineArgs = args;
    }

    public void Execute()
    {
        BaseCommand command = new HelpCommand();

        if (commandLineArgs.Length == 0)
            command.ShowHelpMessage();

        switch (commandLineArgs[0])
        {
            case HelpCommand.Name:
            case HelpCommand.Alias:
                if (commandLineArgs.Length == 1)
                    command.ShowHelpMessage();

                goto case GenerateModeCommand.Alias;

            case GenerateModeCommand.Name:
            case GenerateModeCommand.Alias:
                command = new GenerateModeCommand();
                var commandLineOptions = commandLineArgs.Skip(1).ToArray();
                command.Execute(commandLineOptions);
                break;
        }
    }
}
