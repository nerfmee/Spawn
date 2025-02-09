using Game.Scripts.Services;
using Game.Scripts.Spawn.Entities;

namespace Game.Scripts.Spawn
{
    public interface IEntitySpawnFactory : IService
    {
        DefaultEntity SpawnDefaultEntity();

        void SpawnDefaultEntities(int entitiesCount);

    }
}