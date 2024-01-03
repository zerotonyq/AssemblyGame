using AssemblySystem.Command;
using AssemblySystem.Manager;
using UnityEngine.Events;

namespace AssemblySystem.Views.IBase
{
    public interface ICommandView
    {
        AssemblyManager AssemblyManager { get; }
        AssemblyCommand AssemblyCommand { get; }
        
        public UnityAction<AssemblyCommand> CommandAction { get; set; }
        
        void Initialize(AssemblyManager assemblyManager);
        void TryExecCommand();
    }
}