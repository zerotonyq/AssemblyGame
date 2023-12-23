using AssemblySystem.Command;
using AssemblySystem.Manager;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AssemblySystem.Views
{
    public class ClickObject : MonoBehaviour
    {
        private AssemblyManager _assemblyManager;
        private AssemblyCommand assemblyCommand;

        public UnityAction<AssemblyCommand> CommandAction;
    
        [Inject]
        public void Initialize(AssemblyManager assemblyManager)
        {
            _assemblyManager = assemblyManager;
            CommandAction += _assemblyManager.ExecCommand;
            assemblyCommand = new ClickAssemblyCommand();
        }

        private void OnEnable()
        {
            CommandAction += _assemblyManager.ExecCommand;
        }

        private void OnDisable()
        {
            CommandAction -= _assemblyManager.ExecCommand;
        }

        public void Click()
        {
            CommandAction.Invoke(assemblyCommand);
        }
    }
}