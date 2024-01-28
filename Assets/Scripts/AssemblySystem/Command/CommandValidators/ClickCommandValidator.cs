using System;
using AssemblySystem.Command;
using AssemblySystem.Command.CommandsSO;
using AssemblySystem.Scheme.CommandValidators.Base;
using UnityEngine;

namespace AssemblySystem.Scheme.CommandValidators
{
    public class ClickCommandValidator : IValidatable
    {
        public static void ValidateCommand(ClickCommand command, ClickCommandData commandData)
        {
            if (command.clickView.GetMesh() != commandData.mesh)
                throw new Exception("wrong mesh to click");
            Debug.Log("CLICK WOOOW");
        }
    }
}