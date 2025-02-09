using Game.Scripts.Camera;
using Game.Scripts.Configs;
using Game.Scripts.Mechanics;
using Game.Scripts.Services;
using Game.Scripts.Spawn;
using Game.Scripts.Spawn.Entities;
using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Utility
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UnityConfigs _unityConfigs;
        [SerializeField] private WindowRegistry windowRegistry;
        [SerializeField] private UIRoot uiRoot;
        private int _poolCount;
        private int _spawnCount;
        private int _playerCount;
        private void Start()
        {
            var assetProvider = new AssetProvider();
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            
            // Регистрируем Spawn
            _poolCount = _unityConfigs.SpawnConfig.PoolCount;
            _spawnCount = _unityConfigs.SpawnConfig.SpawnCount;
            _playerCount = _unityConfigs.SpawnConfig.PlayerCount;
            var prefabEntity = assetProvider.LoadEntityPrefab<DefaultEntity>(AssetPath.ENTITY_PATH);
            var playerPrefab = assetProvider.LoadEntityPrefab<Player>(AssetPath.PLAYER_PATH);
            var entityPool = new CustomPool<DefaultEntity>(prefabEntity, _poolCount, transform);
            var playerPool = new CustomPool<Player>(playerPrefab, _playerCount, transform);
            var spawnFabric = new EntitySpawnFactory(entityPool, playerPool);
            AllServices.Container.RegisterSingle<IEntitySpawnFactory>(spawnFabric);
            spawnFabric.SpawnDefaultEntities(_spawnCount);
            
            // Регистрируем GamePlay механики
            var inputSystem = new InputService();
            AllServices.Container.RegisterSingle<IInputService>(inputSystem);
            var movementConfig = _unityConfigs.MovementConfig;
            var movementService = new MovementService(movementConfig.moveSpeed, movementConfig.jumpForce);
            AllServices.Container.RegisterSingle<IMovementService>(movementService);
            var player = spawnFabric.SpawnPlayer(_playerCount, movementService, inputSystem);
            var strategy = AllServices.Container.Single<HorizontalCameraMovementStrategy>();
            strategy.SetFollowTarget(player.transform);

            // Регистрируем WindowManager (UI)
            var windowManager = new WindowController(uiRoot);
            AllServices.Container.RegisterSingle(windowManager);
            windowRegistry.RegisterWindowsForManager(windowManager);
            windowManager.OpenWindow<GameView>();
        }
    }
}
