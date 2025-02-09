using Game.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class GameView : UIElement
    {
        [SerializeField] private Button settingsButton;
        private WindowController _windowController;
        
        public override void Initialize(object data)
        {
            base.Initialize(data);
            Debug.Log("Initializing settings window...");
            _windowController = AllServices.Container.Single<WindowController>();
            settingsButton.onClick.AddListener(OpenSettingsWindow);
        }
        private void OpenSettingsWindow()
        {
            _windowController.OpenWindow<SettingsWindowView>();
        }
    }
}