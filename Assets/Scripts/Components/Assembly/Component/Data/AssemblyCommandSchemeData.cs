using System;
using System.Collections.Generic;
using AssemblySystem.Command;
using AssemblySystem.Command.CommandsSO;
using AssemblySystem.Views;
using UnityEngine;

namespace AssemblySystem.Scheme
{
    [CreateAssetMenu(menuName = "AssemblySystem/CreateAssemblyScheme", fileName = "DefaultAssemblyScheme")]
    public class AssemblyCommandSchemeData : ScriptableObject
    {
        [SerializeField] private List<Comm> assemblySequence;
        public void ValidateCommand(Command.Command command, int commandNumber)
        {
            if (commandNumber >= assemblySequence.Count)
                throw new Exception("Assembly sequence list bounds");
            
            var otherCommandType = command.GetType();
            var schemeCommandType = assemblySequence[commandNumber].GetType();
            
            if (otherCommandType == typeof(ConnectCommand) && 
                schemeCommandType == typeof(ConnectViewConfig))
            {
                ConnectCommandValidator.ValidateCommand(
                    command as ConnectCommand, 
                    assemblySequence[commandNumber] as ConnectViewConfig);
            }
            else if (otherCommandType == typeof(ClickCommand) && 
                     schemeCommandType == typeof(ClickCommandData))
            {
                ClickCommandValidator.ValidateCommand(
                    command as ClickCommand,
                    assemblySequence[commandNumber] as ClickCommandData);
            }
            else
            {
                throw new Exception("wrong command types(or they are not equal)");
            }
        }
    }


}