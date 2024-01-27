using AssemblySystem.Manager.Data;
using AssemblySystem.Scheme;
using UnityEngine;

namespace AssemblySystem.Assembly.Component.Data
{
    [CreateAssetMenu(menuName = "AssemblySystem/AssemblyComponentData", fileName = "DefaultAssemblyComponentData")]
    public class AssemblyComponentData : ScriptableObject
    {
        public AssemblyCommandSchemeData AssemblyCommandSchemeData;
        public AssemblyPartsData AssemblyPartsData;
    }
}