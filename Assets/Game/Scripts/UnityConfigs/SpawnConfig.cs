using Game.Scripts.Factory.Entities;
using UnityEngine;

namespace Game.Scripts.UnityConfigs
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Custom/Spawn Config")]
    public sealed class SpawnConfig : ScriptableObject
    {
        [SerializeField] 
        private DefaultEntity entity;
        
        public DefaultEntity Entity => entity;
    }
}