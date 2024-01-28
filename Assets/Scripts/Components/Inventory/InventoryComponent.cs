using System.Collections.Generic;
using Game.AdvanceInteractionSytem.IAdvanceInteractionToggleable;
using Game.Components.Inventory.Data;
using UnityEngine;

namespace Game.Components.Inventory
{
    public class InventoryComponent : MonoBehaviour, IAdvanceInteractionToggleable
    {
        private GameObject view;
        private List<IItem> _items = new();
        
        public void Init(InventoryInitData inventoryInitData)
        {
            //Instantiate(inventoryInitData, transform).SetActive(false);
            Instantiate(inventoryInitData.ConnectPoint, transform);
            Instantiate(inventoryInitData.GfxPrefab, transform);
        }

        public void PickUp(IItem item)
        {
            if (item == null)
                return;
            
            if (_items.Contains(item))
                Debug.LogError("The item is already in inventory");
            
            
        }

        public void ThrowAway(IItem item)
        {
            
        }
        
        public IReadOnlyList<IItem> Items => _items;
        public void ToggleToAdvanceInteractionMode()
        {
            throw new System.NotImplementedException();
        }
    }
}