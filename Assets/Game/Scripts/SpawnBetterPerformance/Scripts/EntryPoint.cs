using Game.Scripts.SpawnBetterPerformance.Scripts.Factory;
using Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Entities;
using Game.Scripts.SpawnBetterPerformance.Scripts.Mechanics;
using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using Game.Scripts.SpawnBetterPerformance.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.SpawnBetterPerformance.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        private const int POOL_COUNT = 1000;
        private const int SPAWN_COUNT = 500;
        private const int PLAYER_COUNT = 1;
        [SerializeField] private WindowRegistry windowRegistry;
        [SerializeField] private UIRoot uiRoot;
        private void Start()
        {
            var assetProvider = new AssetProvider();
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            
            var prefabEntity = assetProvider.LoadEntityPrefab<DefaultEntity>(AssetPath.ENTITY_PATH);
            var playerPrefab = assetProvider.LoadEntityPrefab<Player>(AssetPath.PLAYER_PATH);

            var entityPool = new CustomPool<DefaultEntity>(prefabEntity, POOL_COUNT, transform);
            var playerPool = new CustomPool<Player>(playerPrefab, PLAYER_COUNT, transform);
            var spawnFabric = new EntitySpawnFactory(entityPool, playerPool);
            AllServices.Container.RegisterSingle<IEntitySpawnFactory>(spawnFabric);
            spawnFabric.SpawnDefaultEntities(SPAWN_COUNT);
            
            // Регистрируем GamePlay механики
            var inputSystem = new InputService();
            AllServices.Container.RegisterSingle<IInputService>(inputSystem);
            var movementService = new MovementService();
            AllServices.Container.RegisterSingle<IMovementService>(movementService);
            spawnFabric.SpawnPlayer(PLAYER_COUNT, movementService, inputSystem);

            // Регистрируем WindowManager (UI)
            var windowManager = new WindowManager(uiRoot);
            AllServices.Container.RegisterSingle(windowManager);
            windowRegistry.RegisterWindowsForManager(windowManager);
            windowManager.OpenWindow<GameView>();
        }
    }
}
