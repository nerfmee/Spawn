using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UnityConfigs
{
    public class UnityConfigs: MonoBehaviour
    {
        [SerializeField] private SpawnConfig _spawnConfig;
        public SpawnConfig SpawnConfig => _spawnConfig;

    }
}