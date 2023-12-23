using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UserInputSystem.SelectObjectSystem.KeyboardMouse.Model
{
    public class MouseSelectHandler : SelectHandler, IDisposable
    {
        private const int RAYCAST_RESULT_COUNT = 5;
        private const float RAY_DISTANCE = 1000000.0f;

        private Camera _mainCamera;
        
        private PlayerInputActions _playerInputActions;

        private InputAction _mousePositionInputAction;
        
        public MouseSelectHandler(PlayerInputActions playerInputActions, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            
            _playerInputActions = playerInputActions;
            
            _mousePositionInputAction = _playerInputActions.Assembly.CursorPosition;
            
            _playerInputActions.Assembly.CursorPosition.Enable();
            _playerInputActions.Assembly.Enable();
            
            _playerInputActions.Assembly.Select.started += Select;
            _playerInputActions.Assembly.Select.canceled += Deselect;
        }

        public override void Select(InputAction.CallbackContext ctx)
        {
            var mousePosition = _mousePositionInputAction.ReadValue<Vector2>();
            
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            
            RaycastHit[] results = new RaycastHit[RAYCAST_RESULT_COUNT];
            
            Physics.RaycastNonAlloc(ray, results, RAY_DISTANCE);
            
            foreach (var re in results)
            {
                if(re.collider == null)
                    continue;
                
                if (re.collider.gameObject.TryGetComponent(out SelectObject selectObject))
                {
                    selectObject.Select();
                    
                    AssignObject(selectObject);

                    _isSelected = true;

                    selectObject.StartCoroutine(MoveObjectCoroutine());
                    
                    break;
                }
            }
        }

        private IEnumerator MoveObjectCoroutine()
        {
            while (IsSelected)
            {
                var currentMousePosition = _mousePositionInputAction.ReadValue<Vector2>();
                var cameraRay = _mainCamera.ScreenPointToRay(currentMousePosition);
                var movePlane = new Plane(_mainCamera.transform.forward, 
                    CurrentSelectedGameObject.transform.position);

                var targetPosition = movePlane.Raycast(cameraRay, out float distance);
                CurrentSelectedGameObject.transform.position = cameraRay.GetPoint(distance);

                yield return null;
            }
        }
        
        private void Deselect(InputAction.CallbackContext ctx)
        {
            Debug.Log( _mousePositionInputAction.ReadValue<Vector2>());
            
            _isSelected = false;

            UnassignObject();
        }
        
        public void Dispose()
        {
            _playerInputActions.Assembly.CursorPosition.Disable();
            _playerInputActions.Assembly.Disable();
            
            _playerInputActions.Assembly.Select.started -= Select;
            _playerInputActions.Assembly.Select.canceled -= Deselect;
            
            _playerInputActions = null;
            _mousePositionInputAction = null;
        }
    }
}