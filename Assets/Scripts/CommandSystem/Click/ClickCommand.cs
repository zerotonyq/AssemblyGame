using AssemblySystem.Views;
using UnityEngine;

namespace AssemblySystem.Command
{
    public class ClickCommand : Command
    {
        
        public override void Execute()
        {
            Debug.Log("Click command executed!");
        }

        public override void Undo()
        {
            Debug.Log("Click command undo!");
        }
    }
}