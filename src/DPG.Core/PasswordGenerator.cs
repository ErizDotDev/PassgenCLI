﻿using System.Text;

namespace DPG.Core;

public class PasswordGenerator
{
    private string[] commandLineArgs;

    private const string HELP_ALIAS = "h";
    private const string HELP_COMMAND_TEXT = "help";
    private const string MODE_ALIAS = "m";
    private const string MODE_COMMAND_TEXT = "mode";

    private const string STANDARD_MODE = "standard";
    private const string ENCODE_MODE = "encode";

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

        switch (commandLineArgs[0])
        {
            case HELP_ALIAS:
            case HELP_COMMAND_TEXT:
                if (commandLineArgs.Length == 1)
                {
                    return ShowHelpMessage();
                }

                return string.Empty;

            case MODE_ALIAS:
            case MODE_COMMAND_TEXT:
                if (commandLineArgs.Contains(HELP_ALIAS) ||
                    commandLineArgs.Contains(HELP_COMMAND_TEXT) ||
                    commandLineArgs.Length == 1)
                {
                    return ShowModeHelpMessage();
                }

                var mode = commandLineArgs[1];

                if (mode != STANDARD_MODE || mode != ENCODE_MODE)
                {
                    return ShowModeHelpMessage();
                }

                return string.Empty;
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

    private string ShowModeHelpMessage()
    {
        var modeHelpMessageBuilder = new StringBuilder();

        // TODO: Update the options section to reflect options to be developed for the mode function.

        modeHelpMessageBuilder.AppendLine("Set the password generator mode to be used.");
        modeHelpMessageBuilder.AppendLine()
            .AppendLine("Usage")
            .AppendLine("dpg mode [standard | encode] [options]")
            .AppendLine();
        modeHelpMessageBuilder.AppendLine("Options: (Note: Options below are only placeholder values)")
            .AppendLine("[-S|--save|--no-save|--save-prod|--save-dev|--save-optional|--save-peer|--save-bundle]");

        return modeHelpMessageBuilder.ToString();
    }
}