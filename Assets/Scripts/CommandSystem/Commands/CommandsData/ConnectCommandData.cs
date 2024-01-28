using AssemblySystem.Command.CommandsSO.Base;
using UnityEngine;

namespace AssemblySystem.Command.CommandsSO
{
    [CreateAssetMenu(menuName = "AssemblySystem/AssemblyCommandSO/ConnectAssemblyCommandSO", fileName = "DefaultConnectACSO")]
    public class ConnectCommandData : CommandData
    {
        public Mesh firstMesh, secondMesh;
    }
}