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
        public List<AssemblyCommandData> AssemblySequence;
        public void ValidateCommand(AssemblyCommand command, int commandNumber)
        {
            if (commandNumber >= AssemblySequence.Count)
                throw new Exception("Assembly sequence list bounds");
            
            var otherCommandType = command.GetType();
            var schemeCommandType = AssemblySequence[commandNumber].GetType();
            
            if (otherCommandType == typeof(ConnectAssemblyCommand) && 
                schemeCommandType == typeof(ConnectAssemblyCommandData))
            {
                ConnectCommandValidator.ValidateCommand(
                    command as ConnectAssemblyCommand, 
                    AssemblySequence[commandNumber] as ConnectAssemblyCommandData);
            }
            else if (otherCommandType == typeof(ClickAssemblyCommand) && 
                     schemeCommandType == typeof(ClickAssemblyCommandData))
            {
                ClickCommandValidator.ValidateCommand(
                    command as ClickAssemblyCommand,
                    AssemblySequence[commandNumber] as ClickAssemblyCommandData);
            }
            else
            {
                throw new Exception("wrong command types(or they are not equal)");
            }
        }
    }


}