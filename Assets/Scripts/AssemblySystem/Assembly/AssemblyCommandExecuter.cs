using System;
using System.Collections.Generic;
using AssemblySystem.Command;
using AssemblySystem.Manager.Data;
using AssemblySystem.Scheme;
using AssemblySystem.Views;
using AssemblySystem.Views.IBase;
using UnityEngine;
using Zenject;

namespace AssemblySystem.Manager
{
    public class AssemblyCommandExecuter
    {
        private Stack<AssemblyCommand> _commands = new Stack<AssemblyCommand>();

        private AssemblyCommandSchemeData _commandSchemeData;
        
        public AssemblyCommandExecuter(AssemblyCommandSchemeData commandSchemeData)
        {
            _commandSchemeData = commandSchemeData;
        }
        
        public void ExecCommand(AssemblyCommand command)
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

        public AssemblyCommand UndoCommand()
        {
            if (_commands.Count == 0)
                return null;
            
            return _commands.Pop();
        }
        public IReadOnlyCollection<AssemblyCommand> Commands => _commands;
        
    }    
}

