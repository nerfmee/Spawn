using Game.Scripts.Factory.Entities;
using Game.Scripts.Services;

namespace Game.Scripts.Factory
{
    public interface IEntitySpawnFactory : IService
    {
        DefaultEntity SpawnDefaultEntity();

        void SpawnDefaultEntities(int entitiesCount);

    }
}