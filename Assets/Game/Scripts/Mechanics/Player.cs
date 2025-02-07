using Game.Scripts.Factory.Entities;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IEntity
    {
        private IMovementService _movementService;
        private InputService _inputService;
        private Rigidbody _rigidbody;

        private Vector3 _moveDirection;

        public void Init(IMovementService movementService, InputService inputService)
        {
            _movementService = movementService;
            _inputService = inputService;
            _rigidbody = GetComponent<Rigidbody>();

            _inputService.OnMove += HandleMove;
            _inputService.OnJump += HandleJump;
        }

        private void HandleMove(Vector2 moveInput)
        {
            _moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        }

        private void HandleJump()
        {
            _movementService.Jump(_rigidbody);
        }

        public void FixedUpdate()
        {
            _movementService.Move(_rigidbody, _moveDirection);
        }

        public Vector2 GetEntitySize()
        {
            return Vector2.one;
        }
    }
}