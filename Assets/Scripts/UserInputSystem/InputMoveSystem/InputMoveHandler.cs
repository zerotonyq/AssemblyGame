using System;
using CommonInterfaces;
using MoveSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.UserInputSystem.InputMoveSystem
{
    public class InputMoveHandler : IDisposable, ISubscribable
    {
        private PlayerInputActions _playerInputActions;
        public Action<Vector2> OnInputChanged;

        private IMovable _currentMovable;

        public void ChangeMovable(IMovable movable)
        {
            if (_currentMovable != null)
            {
                OnInputChanged -= _currentMovable.SetDirectionFromInput;    
            }
            _currentMovable = movable;
            OnInputChanged += _currentMovable.SetDirectionFromInput;
        }
        [Inject]
        public void Init(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
            Subscribe();
        }
        public void AdjustMoveInput(InputAction.CallbackContext ctx)
        {
            OnInputChanged?.Invoke(ctx.ReadValue<Vector2>());
        }

        public void Subscribe()
        {
            _playerInputActions.Gameplay.Enable();
            _playerInputActions.Gameplay.Walk.performed += AdjustMoveInput;
            _playerInputActions.Gameplay.Walk.canceled += AdjustMoveInput;
        }
        public void Unsubscribe()
        {
            _playerInputActions.Gameplay.Disable();
            _playerInputActions.Gameplay.Walk.performed -= AdjustMoveInput;
            _playerInputActions.Gameplay.Walk.canceled -= AdjustMoveInput;
        }
        
        public void Dispose()
        {
            if (_currentMovable != null)
                OnInputChanged -= _currentMovable.SetDirectionFromInput;
            Unsubscribe();
        }
    }
}