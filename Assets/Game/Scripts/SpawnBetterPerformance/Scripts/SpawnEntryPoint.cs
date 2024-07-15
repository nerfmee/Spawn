using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory;
using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Stairs;
using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;

namespace Project.Game.Scripts.SpawnBetterPerformance.Scripts
{
    public class SpawnEntryPoint : MonoBehaviour
    {
       [SerializeField] 
       private UnityConfigs.UnityConfigs unityConfigs;
        private void Start()
        {
            var entity = unityConfigs.SpawnConfig.Entity;
            var entityPool = new CustomPool<DefaultEntity>(entity, 1000, this.transform);
            
            var assetProvider = new AssetProvider();
            var spawnFabric = new EntitySpawnFactory(entityPool,assetProvider);
            
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            AllServices.Container.RegisterSingle<IEntitySpawnFactory>(new EntitySpawnFactory(entityPool,AllServices.Container.Single<IAssetProvider>()));
            spawnFabric.SpawnDefaultEntities(500);
        }
    }
}
