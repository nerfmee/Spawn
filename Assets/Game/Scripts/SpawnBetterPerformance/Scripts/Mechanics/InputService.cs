using System;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Mechanics
{
    public class InputService : IInputService, IDisposable
    {
        private PlayerInput _playerInput;

        public Vector2 MoveInput => _playerInput.Gameplay.Move.ReadValue<Vector2>();
        public bool JumpPressed => _playerInput.Gameplay.Jump.WasPressedThisFrame();

        public InputService()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        public void Dispose()
        {
            _playerInput.Disable();
        }
    }
}