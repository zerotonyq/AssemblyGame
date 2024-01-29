using AssemblySystem.Command;
using AssemblySystem.Manager;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AssemblySystem.Views
{
    public class ClickView : CommandView
    {
        public void Init()
        {
            _command = new ClickCommand();
        }
        public void Click()
        {
            TryExecCommand();
        }
    }
}