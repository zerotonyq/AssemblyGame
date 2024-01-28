using AssemblySystem.Assembly.Component.Data;
using AssemblySystem.Manager.Data;
using AssemblySystem.Scheme;
using AssemblySystem.Views.IBase;
using UnityEngine;
using UnityEngine.Serialization;

namespace AssemblySystem.Manager.Views
{
    public class AssemblyComponent : MonoBehaviour
    {
        private AssemblyCommandSchemeData _schemeData;
     
        private AssemblyPartsData _refferenceParts;
        
        private AssemblyCommandExecutor _assemblyCommandExecutor;

        public void Init(AssemblyComponentData data)
        {
            _schemeData = data.AssemblyCommandSchemeData;
            _refferenceParts = data.AssemblyPartsData;
        }
        private void Start()
        {
            _assemblyCommandExecutor = new AssemblyCommandExecutor(_schemeData);
            Construct();
        }
        
        // TODO: Factory with delays
        private void Construct()
        {
            var prefabs = _refferenceParts.Prefabs;
            foreach (var prefab in prefabs)
            {
                var obj = GameObject.Instantiate(prefab, 
                    Vector3.zero, 
                    Quaternion.identity, 
                    null);
                
                var commandViewComponents = obj.GetComponents<ICommandView>();
                
                foreach (var viewComponent in commandViewComponents)
                {
                    viewComponent.Initialize(_assemblyCommandExecutor);
                }
            }
        }
        
        public bool IsAssemblied => _assemblyCommandExecutor.Commands.Count == _schemeData.AssemblySequence.Count;

    }
    
}