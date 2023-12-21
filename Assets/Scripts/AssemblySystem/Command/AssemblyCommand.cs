
namespace AssemblySystem.Command
{
    public abstract class AssemblyCommand
    {
        public AssemblyCommand()
        {
            
        }

        public abstract void Execute();
        public abstract void Undo();
        
    }

}
