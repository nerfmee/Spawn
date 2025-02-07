using UnityEngine;

namespace Game.Scripts.UnityConfigs
{
    [CreateAssetMenu(menuName = "Game/MovementConfig")]
    public class MovementConfig : ScriptableObject
    {
        public float moveSpeed;
        public float jumpForce;
    }
}