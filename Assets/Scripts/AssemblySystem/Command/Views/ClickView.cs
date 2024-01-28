using AssemblySystem.Command;
using AssemblySystem.Manager;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AssemblySystem.Views
{
    public class ClickView : CommandView<ClickCommand>
    {
        public void Click()
        {
            var currentCommand = _command as ClickCommand;
            currentCommand.clickView = this;
            TryExecCommand();
        }
    }
}