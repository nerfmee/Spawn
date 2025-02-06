using Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Entities;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IEntity
    {
        private IMovementService _movementService;
        private InputService _inputService;
        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;
        private bool _jumpPressed;
        public float MoveSpeed = 5f;
        public float JumpForce = 10f;


        public void Init(IMovementService movementService, InputService inputService)
        {
            _movementService = movementService;
            _inputService = inputService;
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            _moveDirection = new Vector3(_inputService.MoveInput.x, 0, _inputService.MoveInput.y);
            _jumpPressed = _inputService.JumpPressed;
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = new Vector3(_inputService.MoveInput.x, 0, _inputService.MoveInput.y);

            _movementService.Move(_rigidbody, moveDirection);
            if (_jumpPressed)
            {
                _movementService.Jump(_rigidbody, JumpForce);
            }
        }

        public Vector2 GetEntitySize()
        {
            return Vector2.zero;
        }
    }
}