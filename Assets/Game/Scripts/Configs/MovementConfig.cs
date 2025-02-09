using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Game/MovementConfig")]
    public class MovementConfig : ScriptableObject
    {
        public float moveSpeed;
        public float jumpForce;
    }
}