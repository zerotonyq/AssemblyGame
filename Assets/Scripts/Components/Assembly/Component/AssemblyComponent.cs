using System;
using System.Collections.Generic;
using AssemblySystem.Assembly.Component.Data;
using AssemblySystem.Manager.Data;
using AssemblySystem.Scheme;
using AssemblySystem.Views;
using AssemblySystem.Views.Interface;
using Game.Components.Assembly.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace AssemblySystem.Manager.Views
{
    public class AssemblyComponent : MonoBehaviour
    {
        private AssemblyCommandSchemeData _schemeData;
     
        private AssemblyPartsData _referenceParts;
        
        private Stack<Command.Command> _commands = new Stack<Command.Command>();

        public void Init(AssemblyComponentData data)
        {
            _schemeData = data.AssemblyCommandSchemeData;
            _referenceParts = data.AssemblyPartsData;
        }
        private void Start()
        {
            Construct();
        }
        
        // TODO: Factory with delays
        private void Construct()
        {
            var prefabs = _referenceParts.Prefabs;
            foreach (var prefab in prefabs)
            {
                var obj = GameObject.Instantiate(prefab, 
                    Vector3.zero, 
                    Quaternion.identity, 
                    transform);

                var commandViewComponents = obj.GetComponents<CommandView>();
                
                foreach (var viewComponent in commandViewComponents)
                {
                    viewComponent.Subscribe(AddToAssembly);
                    viewComponent.Init(this);
                }
            }
        }

        public void AddToAssembly(Command.Command command)
        {
            int commandNumber = _commands.Count > 0 ? _commands.Count : 0;
            try
            {
                _schemeData.ValidateCommand(command, commandNumber);
            }
            catch (Exception e)
            {
                Debug.LogError("failed to exec command with validator exception: " + e.Message);
                throw;
            }
            command.Execute();
            _commands.Push(command);
        }
        
        public Command.Command UndoCommand()
        {
            if (_commands.Count == 0)
                return null;
            
            return _commands.Pop();
        }
        
        public IReadOnlyCollection<Command.Command> Commands => _commands;
        
       // public bool IsAssemblied => _assemblyCommandExecutor.Commands.Count == _schemeData.AssemblySequence.Count;

    }
    
}