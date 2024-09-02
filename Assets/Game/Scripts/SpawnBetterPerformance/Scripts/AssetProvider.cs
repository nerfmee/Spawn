using Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Entities;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts
{
    public class AssetProvider: IAssetProvider
    {
        public DefaultEntity LoadEntityPrefab(string path)
        {
            DefaultEntity prefab = Resources.Load<DefaultEntity>(path);
            return prefab;
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
