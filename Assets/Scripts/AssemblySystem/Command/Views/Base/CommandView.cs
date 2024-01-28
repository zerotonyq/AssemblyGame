using System;
using AssemblySystem.Command;
using AssemblySystem.Manager;
using AssemblySystem.Manager.Interfaces;
using AssemblySystem.Views.IBase;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AssemblySystem.Views
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class CommandView<T> : MonoBehaviour, ICommandView where T : Command.Command, new()
    {
        protected ICommandExecutor CommandExecutor;
        protected Command.Command _command = new T();

        public UnityAction<Command.Command> CommandAction { get; set; }

        public void Initialize(ICommandExecutor assemblyCommandExecutor)
        {
            this.CommandExecutor = assemblyCommandExecutor;
            Subscribe();
        }

        private void Subscribe()
        {
            CommandAction += CommandExecutor.ExecCommand;
        }

        private void Unsubscribe()
        {
            if (CommandExecutor == null)
                return;
            CommandAction -= CommandExecutor.ExecCommand;
        }

        //unary command
        public void TryExecCommand()
        {
            CommandAction.Invoke(_command);
        }

        //binary command
        public void TryExecCommand(ICommandView other)
        {
            if (CommandExecutor != null)
            {
                CommandAction.Invoke(_command);
            }
            else if (other.AssemblyCommandExecutor != null)
            {
                other.CommandAction.Invoke(_command);
                Initialize(other.AssemblyCommandExecutor);
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
        public ICommandExecutor AssemblyCommandExecutor => CommandExecutor;
        public Command.Command Command => _command;
    }
}
