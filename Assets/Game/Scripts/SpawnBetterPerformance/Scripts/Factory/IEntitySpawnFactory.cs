using Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Entities;
using Game.Scripts.SpawnBetterPerformance.Scripts.Services;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Factory
{
    public interface IEntitySpawnFactory : IService
    {
        DefaultEntity SpawnDefaultEntity();

        void SpawnDefaultEntities(int entitiesCount);

    }
}