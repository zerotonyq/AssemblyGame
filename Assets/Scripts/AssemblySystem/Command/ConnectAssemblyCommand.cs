using System;
using UnityEngine;

namespace AssemblySystem.Command
{
    public class ConnectAssemblyCommand : AssemblyCommand
    {
        public override void Execute()
        {
            Debug.Log("Plug assembly command executed!");
        }

        public override void Undo()
        {
            Debug.Log("Plug assembly command executed back!");
        }
    }   
}
