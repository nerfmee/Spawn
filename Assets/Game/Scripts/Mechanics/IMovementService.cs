using Game.Scripts.Services;
using UnityEngine;

namespace Game.Scripts.Mechanics
{
    public interface IMovementService : IService
    {
        void Move(Rigidbody rb, Vector3 direction);
        void Jump(Rigidbody rigidbody);
    }
}