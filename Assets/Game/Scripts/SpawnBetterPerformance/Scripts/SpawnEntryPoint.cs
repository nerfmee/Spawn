using Game.Scripts.SpawnBetterPerformance.Scripts.Factory;
using Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Entities;
using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts
{
    public class SpawnEntryPoint : MonoBehaviour
    {
        private const int POOL_COUNT = 1000;
        private const int SPAWN_COUNT = 500;
        private void Start()
        {
            var assetProvider = new AssetProvider();
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            
            var prefabEntity = assetProvider.LoadEntityPrefab(AssetPath.ENTITY_PATH);
            var entityPool = new CustomPool<DefaultEntity>(prefabEntity, POOL_COUNT, this.transform);
            var spawnFabric = new EntitySpawnFactory(entityPool);
            
            AllServices.Container.RegisterSingle<IEntitySpawnFactory>(spawnFabric);
            
            spawnFabric.SpawnDefaultEntities(SPAWN_COUNT);
        }
    }
}
