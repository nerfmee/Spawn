using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Stairs;
using UnityEngine;

namespace Project.Game.Scripts.SpawnBetterPerformance.Scripts.UnityConfigs
{
    
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Custom/Spawn Config")]
    public sealed class SpawnConfig : ScriptableObject
    {
        [SerializeField] 
        private DefaultEntity entity;
        
        public DefaultEntity Entity => entity;
    }
}