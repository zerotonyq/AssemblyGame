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
    public class AssemblyManager
    {
        private Stack<AssemblyCommand> _commands = new Stack<AssemblyCommand>();
        private AssemblyCommandScheme _commandScheme;
        private AssemblyPartsSO _partsSO;
        public AssemblyManager(AssemblyCommandScheme commandScheme, AssemblyPartsSO partsSO)
        {
            _commandScheme = commandScheme;
            _partsSO = partsSO;
        }

        
        // TODO: Factory with delays
        public void Construct()
        {
            var prefabs = _partsSO.Prefabs;
            foreach (var prefab in prefabs)
            {
                var obj = GameObject.Instantiate(prefab, 
                    Vector3.zero, 
                    Quaternion.identity, 
                    null);
                var commandViewComponents = obj.GetComponents<ICommandView>();
                
                foreach (var viewComponent in commandViewComponents)
                {
                    viewComponent.Initialize(this);
                }
            }
        }
        public void ExecCommand(AssemblyCommand command)
        {
            try
            {
                var commandNumber = _commands.Count > 0 ? _commands.Count : 0;
                
                _commandScheme.ValidateCommand(command, commandNumber);
                
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

        public bool IsAssemblied => _commands.Count == _commandScheme.AssemblySequence.Count;
    }    
}

