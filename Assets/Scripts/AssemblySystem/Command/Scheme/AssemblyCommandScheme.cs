using System;
using System.Collections.Generic;
using AssemblySystem.Command;
using AssemblySystem.Command.CommandsSO;
using AssemblySystem.Command.CommandsSO.Base;
using AssemblySystem.Scheme.CommandValidators;
using UnityEngine;

namespace AssemblySystem.Scheme
{
    [CreateAssetMenu(menuName = "AssemblySystem/CreateAssemblyScheme", fileName = "DefaultAssemblyScheme")]
    public class AssemblyCommandScheme : ScriptableObject
    {
        public List<AssemblyCommandSO> AssemblySequence;
        public void ValidateCommand(AssemblyCommand command, int commandNumber)
        {
            
            if (commandNumber >= AssemblySequence.Count)
                throw new Exception("Assembly sequence list bounds");
            
            if (command.GetType() == typeof(ConnectAssemblyCommand) && 
                AssemblySequence[commandNumber].GetType() == typeof(ConnectAssemblyCommandSO))
            {
                var connectCommand = command as ConnectAssemblyCommand;
                var connectCommandSO = AssemblySequence[commandNumber] as ConnectAssemblyCommandSO;
                ConnectCommandValidator.ValidateCommand(connectCommand, connectCommandSO);
            }
            else if (command.GetType() == typeof(ClickAssemblyCommand) && 
                     AssemblySequence[commandNumber].GetType() == typeof(ClickAssemblyCommandSO))
            {
                var clickCommand = command as ClickAssemblyCommand;
                var clickCommandSO = AssemblySequence[commandNumber] as ClickAssemblyCommandSO;
                ClickCommandValidator.ValidateCommand(clickCommand, clickCommandSO);
            }
            else
            {
                throw new Exception("wrong command types(or they are not equal)");
            }
        }
    }


}