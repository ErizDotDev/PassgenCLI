namespace DPG.Core.Commands;

internal class BaseCommand
{
    public virtual void Execute(string[] optionInput) { }

    public virtual void ShowHelpMessage() { }
}
