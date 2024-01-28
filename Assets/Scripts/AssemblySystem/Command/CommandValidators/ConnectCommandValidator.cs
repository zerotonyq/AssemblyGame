using System;
using AssemblySystem.Command;
using AssemblySystem.Command.CommandsSO;
using AssemblySystem.Command.CommandsSO.Base;
using AssemblySystem.Scheme.CommandValidators.Base;
using UnityEngine;

namespace AssemblySystem.Scheme.CommandValidators
{
    public  class ConnectCommandValidator : IValidatable
    {
        public static void ValidateCommand(ConnectCommand command, ConnectCommandData commandData)
        {
            if (command.first.GetMesh() == commandData.firstMesh &&
                command.second.GetMesh() == commandData.secondMesh)
            {
                Debug.Log("CONNECT WOOOW");
            }
            
        }
    }
}