namespace AssemblySystem.Manager.Interfaces
{
    public interface ICommandExecutor
    {
        void ExecCommand(Command.Command command);
    }
}