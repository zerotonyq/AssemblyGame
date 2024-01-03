using AssemblySystem.Command.CommandsSO.Base;
using UnityEngine;

namespace AssemblySystem.Command.CommandsSO
{
    [CreateAssetMenu(menuName = "AssemblySystem/AssemblyCommandSO/ConnectAssemblyCommandSO", fileName = "DefaultConnectACSO")]
    public class ConnectAssemblyCommandSO : AssemblyCommandSO
    {
        public Mesh firstMesh, secondMesh;
    }
}