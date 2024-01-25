using System;
using System.Collections;
using AssemblySystem.Views;
using Game.UserInputSystem.SelectObjectSystem;
using Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UserInputSystem.SelectObjectSystem.KeyboardMouse.Handler
{
    public class MouseMoveSelectHandler : SelectHandler, IDisposable
    {
        private const int RAYCAST_RESULT_COUNT = 5;
        private const float RAY_DISTANCE = 1000000.0f;

        private Camera _mainCamera;
        
        private PlayerInputActions _playerInputActions;

        private InputAction _mousePositionInputAction;
        
        public MouseMoveSelectHandler(PlayerInputActions playerInputActions, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            
            _playerInputActions = playerInputActions;
            
            _mousePositionInputAction = _playerInputActions.Assembly.CursorPosition;
            
            _playerInputActions.Assembly.Enable();
            
            _playerInputActions.Assembly.Select.started += Select;
            _playerInputActions.Assembly.Click.performed += Click;
            _playerInputActions.Assembly.Select.canceled += Deselect;
        }

        public void Click(InputAction.CallbackContext ctx)
        {
            var mousePosition = _mousePositionInputAction.ReadValue<Vector2>();
            
            RaycastHit[] results = RaycastHelper.RaycastObjects(
                () => _mainCamera.ScreenPointToRay(mousePosition),
                RAY_DISTANCE,
                RAYCAST_RESULT_COUNT);

            ClickView currentSelectView = RaycastHelper.GetClosest<ClickView>(ref results);

            if (currentSelectView == null)
                return;
            
            currentSelectView.Click();
        }
        public override void Select(InputAction.CallbackContext ctx)
        {
            var mousePosition = _mousePositionInputAction.ReadValue<Vector2>();
            var cameraRay = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit[] results = RaycastHelper.RaycastObjects(
                cameraRay,
                RAY_DISTANCE,
                RAYCAST_RESULT_COUNT);

            SelectView currentSelectView = RaycastHelper.GetClosest<SelectView>(ref results);
            
            if (currentSelectView == null)
                return;
            
            currentSelectView.Select();
                    
            AssignObject(currentSelectView);

            _isSelected = true;
            
            var targetPosition = RaycastHelper.ClosestPointPlaneFacedToCamera(
                _mainCamera,
                CurrentSelectedObjectPosition,
                cameraRay);
            
            var offset = CurrentSelectedObjectPosition - targetPosition;
            
            currentSelectView.StartCoroutine(MoveObjectCoroutine(offset));
        }

        private IEnumerator MoveObjectCoroutine(Vector3 offset)
        {
            while (IsSelected)
            {
                var currentMousePosition = _mousePositionInputAction.ReadValue<Vector2>();
                var cameraRay = _mainCamera.ScreenPointToRay(currentMousePosition);
                var movePlane = new Plane(_mainCamera.transform.forward, 
                    CurrentSelectedObjectPosition);

                movePlane.Raycast(cameraRay, out float distance);
                
                var targetPosition = cameraRay.GetPoint(distance);
                
                CurrentSelectedGameObject.transform.position = offset + targetPosition;
                yield return null;
            }
        }
        
        private void Deselect(InputAction.CallbackContext ctx)
        {
            _isSelected = false;
            
            if (_currentSelectedGameObject == null)
                return;
            
            var currentSelected = _currentSelectedGameObject.GetComponent<SelectView>();
            currentSelected.Deselect();
            
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