using System;
using AssemblySystem.Command;
using AssemblySystem.Manager;
using AssemblySystem.Views.IBase;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AssemblySystem.Views
{
    [RequireComponent(typeof(MeshFilter))]
    public class CommandView<T> : MonoBehaviour, ICommandView where T : AssemblyCommand, new()
    {
        protected AssemblyCommandExecuter assemblyCommandExecuter;
        protected AssemblyCommand assemblyCommand = new T();

        public UnityAction<AssemblyCommand> CommandAction { get; set; }

        public void Initialize(AssemblyCommandExecuter assemblyCommandExecuter)
        {
            this.assemblyCommandExecuter = assemblyCommandExecuter;
            Subscribe();
        }

        private void Subscribe()
        {
            CommandAction += assemblyCommandExecuter.ExecCommand;
        }

        private void Unsubscribe()
        {
            if (assemblyCommandExecuter == null)
                return;
            CommandAction -= assemblyCommandExecuter.ExecCommand;
        }

        //unary command
        public void TryExecCommand()
        {
            CommandAction.Invoke(assemblyCommand);
        }

        //binary command
        public void TryExecCommand(ICommandView other)
        {
            if (assemblyCommandExecuter != null)
            {
                CommandAction.Invoke(assemblyCommand);
            }
            else if (other.AssemblyCommandExecuter != null)
            {
                other.CommandAction.Invoke(assemblyCommand);
                Initialize(other.AssemblyCommandExecuter);
            }
            else
            {
                throw new Exception("there is no assembly to exec command on");
            }
        }
        
        public void OnDestroy()
        {
            Unsubscribe();
        }

        public Mesh GetMesh() => GetComponent<MeshFilter>().sharedMesh;
        public AssemblyCommandExecuter AssemblyCommandExecuter => assemblyCommandExecuter;
        public AssemblyCommand AssemblyCommand => assemblyCommand;
    }
}
