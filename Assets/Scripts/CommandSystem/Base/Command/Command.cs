
using UnityEngine;

namespace AssemblySystem.Command
{
    public abstract class Command
    {
        
        public virtual void Execute()
        {
            Debug.Log("default assembly command");
        }

        public virtual void Undo()
        {
            Debug.Log("undo default assembly command");
        }
        
    }

}
