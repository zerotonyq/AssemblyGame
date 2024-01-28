using AssemblySystem.Command.CommandsSO.Base;
using UnityEngine;

namespace AssemblySystem.Command.CommandsSO
{
    [CreateAssetMenu(menuName = "AssemblySystem/AssemblyCommandSO/ClickAssemblyCommandSO", fileName = "DefaultClickACSO")]
    public class ClickCommandData : CommandData
    {
        public Mesh mesh;
    }
}