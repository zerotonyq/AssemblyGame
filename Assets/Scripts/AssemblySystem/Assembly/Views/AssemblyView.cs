using AssemblySystem.Manager.Data;
using AssemblySystem.Scheme;
using UnityEngine;

namespace AssemblySystem.Manager.Views
{
    public class AssemblyView : MonoBehaviour
    {
        [SerializeField]
        private AssemblyCommandScheme scheme;

        [SerializeField] 
        private AssemblyPartsSO parts;
        
        private AssemblyManager _manager;
        
        private void Start()
        {
            _manager = new AssemblyManager(scheme, parts);
            _manager.Construct();
        }

    }
    
}