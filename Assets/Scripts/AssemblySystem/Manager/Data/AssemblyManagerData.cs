using System.Collections.Generic;
using AssemblySystem.Assembly.Component.Data;
using UnityEngine;

namespace AssemblySystem.Manager.Data
{
    [CreateAssetMenu(menuName = "AssemblySystem/AssemblyManagerData", fileName = "DefaultAssemblyManagerData")]
    public class AssemblyManagerData : ScriptableObject
    {
        public List<AssemblyComponentData> ComponentDatas;
    }
}