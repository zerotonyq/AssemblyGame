using AssemblySystem.Command;
using AssemblySystem.Views;
using UnityEngine;

namespace Game.Components.Inventory.Data
{
    [CreateAssetMenu(menuName = "Inventory/InventoryData", fileName = "DefaultInventoryData")]
    public class InventoryInitData : ScriptableObject
    {
        [SerializeField] private GameObject _GFXPrefab;

        [SerializeField] private ConnectView _cell;

        [SerializeField] private int _inventoryCapacity;
        
        public GameObject GfxPrefab => _GFXPrefab;

        public ConnectView Cell => _cell;
        
        public int InventoryCapacity => _inventoryCapacity;
    }
}