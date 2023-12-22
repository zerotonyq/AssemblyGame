using System;
using System.Collections.Generic;
using AssemblySystem.Command;
using UnityEngine;

namespace AssemblySystem.Manager
{
    public class AssemblyManager
    {
        private Stack<AssemblyCommand> _commands = new Stack<AssemblyCommand>();

        public void ExecCommand(AssemblyCommand command)
        {
            try
            {
                command.Execute();
            }
            catch (Exception e)
            {
                Debug.LogError("failed to exec command with exception: " + e);
                throw;
            }
            _commands.Push(command);
        }

        public AssemblyCommand UndoCommand()
        {
            if (_commands.Count == 0)
                return null;
            
            return _commands.Pop();
        }
    }    
}

