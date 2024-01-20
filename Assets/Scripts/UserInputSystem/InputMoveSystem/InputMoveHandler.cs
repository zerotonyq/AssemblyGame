using System;
using CommonInterfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace UserInputSystem.InputMoveSystem
{
    public class InputMoveHandler : IDisposable, ISubscribable
    {
        private PlayerInputActions _playerInputActions;
        public UnityAction<Vector2> InputChanged;

        [Inject]
        public void Init(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
            Subscribe();
        }
        public void AdjustMoveInput(InputAction.CallbackContext ctx)
        {
            InputChanged?.Invoke(ctx.ReadValue<Vector2>());
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
            Unsubscribe();
        }
    }
}