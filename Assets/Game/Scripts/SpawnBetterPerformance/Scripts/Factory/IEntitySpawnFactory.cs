using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Stairs;
using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Services;

namespace Project.Game.Scripts.SpawnBetterPerformance.Scripts.Factory
{
    public interface IEntitySpawnFactory : IService
    {
        DefaultEntity SpawnDefaultEntity();

        void SpawnDefaultEntities(int entitiesCount);

    }
}