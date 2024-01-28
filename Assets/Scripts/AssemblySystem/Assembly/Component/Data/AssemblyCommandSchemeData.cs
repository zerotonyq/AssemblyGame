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
    public class AssemblyCommandSchemeData : ScriptableObject
    {
        public List<CommandData> AssemblySequence;
        public void ValidateCommand(Command.Command command, int commandNumber)
        {
            if (commandNumber >= AssemblySequence.Count)
                throw new Exception("Assembly sequence list bounds");
            
            var otherCommandType = command.GetType();
            var schemeCommandType = AssemblySequence[commandNumber].GetType();
            
            if (otherCommandType == typeof(ConnectCommand) && 
                schemeCommandType == typeof(ConnectCommandData))
            {
                ConnectCommandValidator.ValidateCommand(
                    command as ConnectCommand, 
                    AssemblySequence[commandNumber] as ConnectCommandData);
            }
            else if (otherCommandType == typeof(ClickCommand) && 
                     schemeCommandType == typeof(ClickCommandData))
            {
                ClickCommandValidator.ValidateCommand(
                    command as ClickCommand,
                    AssemblySequence[commandNumber] as ClickCommandData);
            }
            else
            {
                throw new Exception("wrong command types(or they are not equal)");
            }
        }
    }


}