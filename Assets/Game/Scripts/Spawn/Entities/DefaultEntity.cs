using UnityEngine;

namespace Game.Scripts.Factory.Entities
{
    public class DefaultEntity : MonoBehaviour, IEntity
    {
        public Vector2 GetEntitySize()
        {
            return new Vector2(2, 2);
        }
    }
}