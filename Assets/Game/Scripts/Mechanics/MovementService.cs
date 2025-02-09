using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class MovementService : IMovementService
    {
        private float _moveSpeed;
        private float _jumpForce;
        private int _mask = LayerMask.GetMask("Ground");
        public MovementService(float moveSpeed, float jumpForce)
        {
            _moveSpeed = moveSpeed;
            _jumpForce = jumpForce;
        }

        public void Move(Rigidbody rigidbody, Vector3 moveDirection)
        {
            Vector3 velocity = rigidbody.velocity;
            velocity.x = moveDirection.x * _moveSpeed;
            velocity.z = moveDirection.z * _moveSpeed;
            rigidbody.velocity = velocity;
        }

        public void Jump(Rigidbody rigidbody)
        {
            if (IsGrounded(rigidbody))
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
                rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        private bool IsGrounded(Rigidbody rigidbody)
        {
            return Physics.Raycast(rigidbody.position, Vector3.down, 0.8f, _mask);
        }
    }

}
