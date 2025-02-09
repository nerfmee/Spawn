using Game.Scripts.Spawn.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Custom/Spawn Config")]
    public sealed class SpawnConfig : ScriptableObject
    {
        [SerializeField]
        private int poolCount = 1000;
        public int PoolCount => poolCount;
        [SerializeField]
        private int spawnCount = 500;
        public int SpawnCount => spawnCount;
        [SerializeField]
        private int playerCount = 1;
        public int PlayerCount => playerCount;
    }
}