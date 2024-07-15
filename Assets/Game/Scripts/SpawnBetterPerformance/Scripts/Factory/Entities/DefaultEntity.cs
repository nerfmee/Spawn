using Project.Game.Scripts.SpawnBetterPerformance.Factory.Stairs;
using UnityEngine;

namespace Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Stairs
{
    public class DefaultEntity : MonoBehaviour, IEntity
    {
        public Vector2 GetEntitySize()
        {
            return new Vector2(2, 2);
        }
    }
}