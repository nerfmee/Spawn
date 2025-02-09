using Game.Scripts.Spawn.Entities;
using UnityEngine;

namespace Game.Scripts.Utility
{
    public class AssetProvider: IAssetProvider
    {
        public T LoadEntityPrefab<T>(string path) where T : MonoBehaviour, IEntity
        {
            return Resources.Load<T>(path);
        }
        
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        
        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}
