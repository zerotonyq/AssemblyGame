using System;
using AssemblySystem.Command;
using AssemblySystem.Manager;
using AssemblySystem.Views.IBase;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AssemblySystem.Views
{
    [RequireComponent(typeof(MeshFilter))]
    public class CommandView<T> : MonoBehaviour, ICommandView where T : AssemblyCommand, new()
    {
        protected AssemblyManager assemblyManager;
        protected AssemblyCommand assemblyCommand = new T();

        public UnityAction<AssemblyCommand> CommandAction { get; set; }

        public void Initialize(AssemblyManager assemblyManager)
        {
            this.assemblyManager = assemblyManager;
            Subscribe();
        }

        private void Subscribe()
        {
            CommandAction += assemblyManager.ExecCommand;
        }

        private void Unsubscribe()
        {
            if (assemblyManager == null)
                return;
            CommandAction -= assemblyManager.ExecCommand;
        }

        //unary command
        public void TryExecCommand()
        {
            CommandAction.Invoke(assemblyCommand);
        }

        //binary command
        public void TryExecCommand(ICommandView other)
        {
            if (assemblyManager != null)
            {
                CommandAction.Invoke(assemblyCommand);
            }
            else if (other.AssemblyManager != null)
            {
                other.CommandAction.Invoke(assemblyCommand);
                Initialize(other.AssemblyManager);
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
        public AssemblyManager AssemblyManager => assemblyManager;
        public AssemblyCommand AssemblyCommand => assemblyCommand;
    }
}
