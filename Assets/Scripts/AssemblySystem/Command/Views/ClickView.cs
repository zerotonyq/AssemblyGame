using AssemblySystem.Command;
using AssemblySystem.Manager;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AssemblySystem.Views
{
    public class ClickView : CommandView<ClickAssemblyCommand>
    {
        public void Click()
        {
            var currentCommand = assemblyCommand as ClickAssemblyCommand;
            currentCommand.clickView = this;
            TryExecCommand();
        }
    }
}