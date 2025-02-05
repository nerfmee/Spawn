using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    public class GameView : UIElement
    {
        [SerializeField] private Button settingsButton;
        private WindowManager _windowManager;
        
        public override void Initialize(object data)
        {
            base.Initialize(data);
            Debug.Log("Initializing settings window...");
            _windowManager = AllServices.Container.Single<WindowManager>();
            settingsButton.onClick.AddListener(OpenSettingsWindow);
        }
        private void OpenSettingsWindow()
        {
            _windowManager.OpenWindow<SettingsWindowView>();
        }
    }
}