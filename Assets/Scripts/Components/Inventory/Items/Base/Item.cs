using AssemblySystem.Manager;
using AssemblySystem.Manager.Views;

namespace Game.Components.Inventory
{
    public interface IItem
    {
        AssemblyManager AssemblyManager { get; }
    }
}