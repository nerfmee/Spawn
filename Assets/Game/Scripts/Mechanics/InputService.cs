using System;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class InputService : IInputService, IDisposable
    {
        private PlayerInput _inputActions;
        public event Action<Vector2> OnMove;
        public event Action OnJump;

        private Vector2 _moveInput;
        private bool _isJumping;

        public InputService()
        {
            _inputActions = new PlayerInput();
        
            _inputActions.Gameplay.Move.started += ctx => UpdateMoveInput(ctx.ReadValue<Vector2>());
            _inputActions.Gameplay.Move.performed += ctx => UpdateMoveInput(ctx.ReadValue<Vector2>());
            _inputActions.Gameplay.Move.canceled += ctx => UpdateMoveInput(Vector2.zero);

            _inputActions.Gameplay.Jump.started += ctx => OnJump?.Invoke();
        
            _inputActions.Enable();
        }

        private void UpdateMoveInput(Vector2 moveInput)
        {
            _moveInput = moveInput;
            OnMove?.Invoke(_moveInput);
        }

        public void Dispose()
        {
            _inputActions.Disable();
        }
    }
}