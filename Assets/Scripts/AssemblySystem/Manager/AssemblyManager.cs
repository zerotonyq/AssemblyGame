using System.Collections.Generic;
using AssemblySystem.Manager.Data;
using AssemblySystem.Manager.Views;
using UnityEngine;

namespace AssemblySystem.Manager
{
    public class AssemblyManager
    {
        public List<AssemblyComponent> assemblyComponents = new ();

        private AssemblyManagerData _managerData;
        
        private Transform _parentItem;

        public AssemblyManager(AssemblyManagerData data, Transform parentItem)
        {
            _parentItem = parentItem;
            _managerData = data;
        }
        
        public void CreateComponents()
        {
            for (int i = 0; i < _managerData.ComponentDatas.Count; ++i)
            {
                var currentComponent = new GameObject().AddComponent<AssemblyComponent>();;
                
                currentComponent.transform.parent = _parentItem;
                
                currentComponent.Init(_managerData.ComponentDatas[i]);
            }
        }
    }
}