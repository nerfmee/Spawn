using Project.Game.Scripts.SpawnBetterPerformance.Factory;
using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Stairs;
using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;

namespace Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory
{
    public class EntitySpawnFactory : IEntitySpawnFactory
    {
        private readonly CustomPool<DefaultEntity> _defaultStairsPool;
  
        private readonly IAssetProvider _assetProvider;
        private Vector2 _spawnPosition;

        public EntitySpawnFactory(CustomPool<DefaultEntity> defaultStairsPool, IAssetProvider assetProvider)
        {
            _defaultStairsPool = defaultStairsPool;
         
            _assetProvider = assetProvider;
        }

        public DefaultEntity SpawnDefaultEntity()
        {
            DefaultEntity defaultEntity = _defaultStairsPool.Get();
            SpawnInChessOrder(defaultEntity);
            defaultEntity.transform.position = _spawnPosition;
            return defaultEntity;
        }

        private int _counterForChessOrder = 0;
        private void SpawnInChessOrder(DefaultEntity defaultEntity)
        {
            if (_counterForChessOrder == 0)
            {
                _counterForChessOrder++;
                _spawnPosition += defaultEntity.GetEntitySize();
            }
            else
            {
                _counterForChessOrder--;
                _spawnPosition = new Vector2(_spawnPosition.x + defaultEntity.GetEntitySize().x,
                    _spawnPosition.y - defaultEntity.GetEntitySize().y);
            }
        }

        private void SpawnLikeStair(DefaultEntity defaultEntity)
        {
            _spawnPosition += defaultEntity.GetEntitySize();
        }
        
        public void SpawnDefaultEntities(int entitiesCount)
        {
            for (int i = 0; i < entitiesCount; i++)
            {
                SpawnDefaultEntity();
            }
        }
        
        public void SpawnCustomStair()
        {
            throw new System.NotImplementedException();
        }

    }
}