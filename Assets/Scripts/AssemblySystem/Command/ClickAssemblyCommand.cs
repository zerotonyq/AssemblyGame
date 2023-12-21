using UnityEngine;

namespace AssemblySystem.Command
{
    public class ClickAssemblyCommand : AssemblyCommand
    {
        public override void Execute()
        {
            Debug.Log("Click command executed!");
        }

        public override void Undo()
        {
            Debug.Log("Click command executed back!");
        }
    }
}