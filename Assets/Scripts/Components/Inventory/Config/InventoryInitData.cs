using AssemblySystem.Command;
using AssemblySystem.Views;
using AssemblySystem.Views.IBase;
using UnityEngine;

namespace Game.Components.Inventory.Data
{
    [CreateAssetMenu(menuName = "Inventory/InventoryData", fileName = "DefaultInventoryData")]
    public class InventoryInitData : ScriptableObject
    {
        [SerializeField] private GameObject _GFXPrefab;

        [SerializeField] private GameObject _connectPoint;
        
        public GameObject GfxPrefab => _GFXPrefab;

        public GameObject ConnectPoint => _connectPoint;
    }
}