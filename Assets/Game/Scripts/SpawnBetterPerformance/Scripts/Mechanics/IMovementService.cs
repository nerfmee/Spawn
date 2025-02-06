
using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Mechanics
{
    public interface IMovementService: IService
    {
        void Move(Rigidbody rb, Vector3 direction);
        void Jump(Rigidbody rb, float jumpForce); }
}