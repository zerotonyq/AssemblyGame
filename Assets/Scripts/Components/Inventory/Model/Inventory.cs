using System;
using System.Collections.Generic;
using Game.AdvanceInteractionSytem.IAdvanceInteractionToggleable;
using UnityEngine;

namespace Game.Components.Inventory.View
{
    public class Inventory
    {
        
        private readonly List<IItem> _items;
        private readonly int _inventoryCapacity;

        public Inventory(int inventoryCapacity)
        {
            _inventoryCapacity = inventoryCapacity;
            _items = new();
        }
        public void AddItem(IItem item)
        {
            try
            {
                ValidateItem(item);
            }
            catch (Exception e)
            {
                Debug.LogError("Inventory error: " + e.Message);
                throw;
            }
            
            _items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            try
            {
                ValidateItem(item);
            }
            catch (Exception e)
            {
                Debug.LogError("Inventory error: " + e.Message);
                throw;
            }

            _items.Remove(item);
        }

        private void ValidateItem(IItem item)
        {
            if (item == null)
                throw new Exception("no item");

            if (_items.Contains(item))
                throw new Exception("already in inventory");

            if (_items.Count >= _inventoryCapacity)
                throw new Exception("capacity is exceeded");
        }
        
        public IReadOnlyList<IItem> Items => _items;
    }
}