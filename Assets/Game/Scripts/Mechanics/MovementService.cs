using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public class MovementService : IMovementService
    {
        private float _moveSpeed;
        private float _jumpForce;

        public MovementService(float moveSpeed, float jumpForce)
        {
            _moveSpeed = moveSpeed;
            _jumpForce = jumpForce;
        }

        public void Move(Rigidbody rigidbody, Vector3 moveDirection)
        {
            if (moveDirection.magnitude > 0.1f)
            {
                Vector3 move = moveDirection * _moveSpeed * Time.fixedDeltaTime;
                rigidbody.MovePosition(rigidbody.position + move);
            }
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
            return Physics.Raycast(rigidbody.position, Vector3.down, 0.8f, LayerMask.GetMask("Ground"));
        }
    }

}
