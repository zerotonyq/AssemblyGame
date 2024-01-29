using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblySystem.Views
{
    public class CommandView : MonoBehaviour
    {
        protected Command.Command _command;

        private List<Action<Command.Command>> _subscribers = new ();
        
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
        
        public Action<Command.Command> OnTryExecCommand { get; set; }
    }
}
