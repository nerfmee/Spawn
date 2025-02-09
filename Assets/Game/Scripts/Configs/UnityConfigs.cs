using UnityEngine;

namespace Game.Scripts.Configs
{
    public class UnityConfigs: MonoBehaviour
    {
        [SerializeField] private SpawnConfig _spawnConfig;
        public SpawnConfig SpawnConfig => _spawnConfig;

        [SerializeField] private MovementConfig _movementConfig;
        public MovementConfig MovementConfig => _movementConfig;
    }
}