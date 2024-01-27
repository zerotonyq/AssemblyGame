using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.Inventory
{
    public class InventoryComponent : MonoBehaviour
    {

        private List<IItem> _items = new();

        public void Init()
        {
            
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
    }
}