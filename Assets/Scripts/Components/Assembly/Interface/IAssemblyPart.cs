using AssemblySystem.Manager.Views;
using AssemblySystem.Views.Interface;

namespace Game.Components.Assembly.Interface
{
    public interface IAssemblyPart
    {
        AssemblyComponent AssemblyComponent { get; }

        bool IsPartOfAssembly { get; }
    }
}