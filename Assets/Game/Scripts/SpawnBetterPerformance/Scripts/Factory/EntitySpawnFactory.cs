using Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Entities;
using Game.Scripts.SpawnBetterPerformance.Scripts.Mechanics;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Factory
{
    public class EntitySpawnFactory : IEntitySpawnFactory
    {
        private readonly CustomPool<DefaultEntity> _defaultEntityPool;
        private readonly CustomPool<Player> _playerPool;

        private Vector2 _spawnPosition;

        public EntitySpawnFactory(CustomPool<DefaultEntity> defaultEntityPool, CustomPool<Player> playerPool)
        {
            _defaultEntityPool = defaultEntityPool;
            _playerPool = playerPool;
        }

        public DefaultEntity SpawnDefaultEntity()
        {
            DefaultEntity defaultEntity = _defaultEntityPool.Get();
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

        private void SpawnLikeStair(IEntity defaultEntity)
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

        public void SpawnPlayer(int playersCount, IMovementService movementService, InputService inputService)
        {
            _spawnPosition = new Vector2(2,10);
            
            for (int i = 0; i < playersCount; i++)
            {
                Player player = _playerPool.Get();
                player.Init(movementService, inputService);
                SpawnLikeStair(player);
                player.transform.position = _spawnPosition;
            }
        }
    }
}