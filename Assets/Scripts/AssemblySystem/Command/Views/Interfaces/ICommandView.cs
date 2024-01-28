using AssemblySystem.Command;
using AssemblySystem.Manager;
using AssemblySystem.Manager.Interfaces;
using UnityEngine.Events;

namespace AssemblySystem.Views.IBase
{
    public interface ICommandView
    {
        ICommandExecutor AssemblyCommandExecutor { get; }
        Command.Command Command { get; }
        
        public UnityAction<Command.Command> CommandAction { get; set; }
        
        void Initialize(ICommandExecutor assemblyCommandExecutor);
        void TryExecCommand();
    }
}