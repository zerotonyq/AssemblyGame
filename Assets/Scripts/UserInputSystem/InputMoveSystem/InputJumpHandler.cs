using System;
using CommonInterfaces;
using Game.Components.Jumping.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.UserInputSystem.InputMoveSystem
{
    public class InputJumpHandler : IDisposable, ISubscribable
    {
        private PlayerInputActions _playerInputActions;
        
        public Action OnJumpInvoked;
        
        private IJumpable _currentJumpable;
        
        [Inject]
        public void Init(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
            Subscribe();
        }

        public void ChangeJumpable(IJumpable jumpable)
        {
            if (_currentJumpable != null)
            {
                OnJumpInvoked -= _currentJumpable.Jump;
            }

            _currentJumpable = jumpable;
            OnJumpInvoked += _currentJumpable.Jump;
        }

        public void AddJumpInput(InputAction.CallbackContext ctx)
        {
            OnJumpInvoked?.Invoke();   
        }

        public void Subscribe()
        {
            _playerInputActions.Gameplay.Enable();
            _playerInputActions.Gameplay.Jump.started += AddJumpInput;
        }

        public void Unsubscribe()
        {
            _playerInputActions.Gameplay.Disable();
            _playerInputActions.Gameplay.Jump.started -= AddJumpInput;
        }
        
        public void Dispose()
        {
            if (_currentJumpable != null)
                OnJumpInvoked -= _currentJumpable.Jump;
            Unsubscribe();
        }
    }
}