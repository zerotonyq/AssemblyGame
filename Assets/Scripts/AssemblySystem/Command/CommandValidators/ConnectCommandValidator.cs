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
        public static void ValidateCommand(ConnectAssemblyCommand command, ConnectAssemblyCommandSO commandSo)
        {
            if (command.first.GetMesh() == commandSo.firstMesh &&
                command.second.GetMesh() == commandSo.secondMesh)
            {
                Debug.Log("CONNECT WOOOW");
            }
            
        }
    }
}