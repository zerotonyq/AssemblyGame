using System;
using System.Collections;
using System.Collections.Generic;
using AssemblySystem.Command;
using Game.Components.Inventory.Data;
using Game.Components.Inventory.Items.Enum;
using UnityEngine;

namespace Game.Components.Inventory
{
    public class InventoryComponent : MonoBehaviour
    {
        private View.Inventory model;

        private Dictionary<ConnectView, CellState> _cells;

        private InventoryInitData _initData;
        
        public void Init(InventoryInitData inventoryInitData)
        {
            _initData = inventoryInitData;
            
            model = new View.Inventory(inventoryInitData.InventoryCapacity);
            
            CreatePoints(inventoryInitData.InventoryCapacity);
            
            //govno
            Instantiate(inventoryInitData.GfxPrefab, transform);
        }

        private void CreatePoints(int count)
        {
            StartCoroutine(CreatePointsCoroutine(count));
        }
        
        private IEnumerator CreatePointsCoroutine(int count, float delta = 0.3f)
        {
            for (int i = 0; i < count; ++i)
            {
                var point = Instantiate(_initData.Cell, transform);
                
                _cells[point] = CellState.Free;

                point.OnTryExecCommand += PlaceItem;
                
                yield return new WaitForSeconds(delta);
            }
        }

        private void PlaceItem(Command command)
        {
            try
            {
                var connectCommand = command as ConnectCommand;

                if (!CheckInventoryPlace(connectCommand._jack.GetComponent<ConnectView>()))
                    return;
                
                command.Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("cannot place item: " + ex.Message);
            }
        }

        private void ThrowOutItem(IItem item)
        {
            
        }

        private bool CheckInventoryPlace(ConnectView connectView)
        {
            switch (_cells[connectView])
            {
                case CellState.Busy:
                    return false;
                case CellState.Free:
                    return true;
                default:
                    throw new Exception("cant check inventory place");
            }
        }
    }
}