using System;
using System.Collections.Generic;
using AssemblySystem.Manager.Views;
using AssemblySystem.Views.Interface;
using Game.Components.Assembly.Interface;
using UnityEngine;

namespace AssemblySystem.Views
{
    public class CommandView : MonoBehaviour, ICommandView, IAssemblyPart
    {
        protected Command.Command _command;

        private List<Action<Command.Command>> _subscribers = new ();
        
        private Action<Command.Command> OnTryExecCommand { get; set; }
        
        protected void TryExecCommand()
        {
            OnTryExecCommand.Invoke(_command);
        }
        
        public void Subscribe(Action<Command.Command> subscriber)
        {
            OnTryExecCommand += subscriber;
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(Action<Command.Command> subscriber)
        {
            OnTryExecCommand -= subscriber;
            _subscribers.Remove(subscriber);
        }

        public void Init(AssemblyComponent assemblyComponent) => AssemblyComponent = assemblyComponent;
        public AssemblyComponent AssemblyComponent { get; private set; }

        public bool IsPartOfAssembly => AssemblyComponent != null;
    }
}
