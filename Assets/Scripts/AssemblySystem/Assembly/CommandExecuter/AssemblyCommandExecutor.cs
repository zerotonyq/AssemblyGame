using System;
using System.Collections.Generic;
using AssemblySystem.Command;
using AssemblySystem.Manager.Data;
using AssemblySystem.Manager.Interfaces;
using AssemblySystem.Scheme;
using AssemblySystem.Views;
using AssemblySystem.Views.IBase;
using UnityEngine;
using Zenject;

namespace AssemblySystem.Manager
{
    public class AssemblyCommandExecutor : ICommandExecutor
    {
        private Stack<Command.Command> _commands = new Stack<Command.Command>();

        private AssemblyCommandSchemeData _commandSchemeData;
        
        public AssemblyCommandExecutor(AssemblyCommandSchemeData commandSchemeData)
        {
            _commandSchemeData = commandSchemeData;
        }
        
        public void ExecCommand(Command.Command command)
        {
            try
            {
                var commandNumber = _commands.Count > 0 ? _commands.Count : 0;
                
                _commandSchemeData.ValidateCommand(command, commandNumber);
                
                command.Execute();
                
                _commands.Push(command);
            }
            catch (Exception e)
            {
                Debug.LogError("failed to exec command with exception: " + e);
                throw;
            }
        }

        public Command.Command UndoCommand()
        {
            if (_commands.Count == 0)
                return null;
            
            return _commands.Pop();
        }
        public IReadOnlyCollection<Command.Command> Commands => _commands;
        
    }
    
}

