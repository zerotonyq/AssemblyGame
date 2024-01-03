using System.Collections.Generic;
using UnityEngine;

namespace AssemblySystem.Manager.Data
{
    [CreateAssetMenu(menuName = "AssemblySystem/AssemblyPartsSO", fileName = "DefaultAssemblyPartsSO")]
    public class AssemblyPartsSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> prefabViews;
        public IReadOnlyList<GameObject> Prefabs => prefabViews;
    }
}