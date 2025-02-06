using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Mechanics
{
    public class MovementService : IMovementService
    {
        private readonly float _speed;

        public MovementService(float speed = 5f)
        {
            _speed = speed;
        }

        public void Move(Rigidbody rb, Vector3 direction)
        {
            Vector3 velocity = direction * _speed;
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }

        public void Jump(Rigidbody rb, float jumpForce)
        {
            if (IsGrounded(rb))
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
        }
        
        private bool IsGrounded(Rigidbody rb)
        {
            return Physics.Raycast(rb.position, Vector3.down, 1.1f);
        }
    }
}