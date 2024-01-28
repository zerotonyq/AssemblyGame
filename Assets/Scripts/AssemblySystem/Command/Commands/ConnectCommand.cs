using System;
using UnityEngine;

namespace AssemblySystem.Command
{
    public class ConnectCommand : Command
    {
        public ConnectView first, second;
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
