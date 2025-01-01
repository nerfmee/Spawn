using System.Collections;
using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using Game.Scripts.SpawnBetterPerformance.Scripts.UI.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    public class SettingsWindowController : WindowController
    {
        [SerializeField] private Button settingsButton;
        [SerializeField] private WindowEffect windowEffect;
        private WindowManager _windowManager;

        public override void Initialize(object data)
        {
            base.Initialize(data);
            Debug.Log("Initializing settings window...");
            settingsButton.onClick.AddListener(() =>
            {
                PlayCloseAnimation(CloseWindow);
            });        
        }

        public override void PlayOpenAnimation(System.Action onComplete)
        {
            StartCoroutine(AnimateDissolve(true, onComplete));
            base.PlayOpenAnimation(onComplete);
            Debug.Log("Settings window opening...");
        }
        
        public override void PlayCloseAnimation(System.Action onComplete)
        {
            StartCoroutine(AnimateDissolve(false, onComplete));
            base.PlayCloseAnimation(onComplete);
            Debug.Log("Settings window closing...");
          
        }

        private void CloseWindow()
        {
            _windowManager = AllServices.Container.Single<WindowManager>();
            _windowManager.CloseWindow<SettingsWindowController>();
            
        }
        
        private IEnumerator AnimateDissolve(bool isOpening, System.Action onComplete)
        {
            float duration = windowEffect.duration;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / duration);

                float dissolveValue = isOpening ? progress : 1 - progress;
                windowEffect.dissolveMaterial.SetFloat("_DissolveProgress", dissolveValue);

                yield return null;
            }

            windowEffect.dissolveMaterial.SetFloat("_DissolveProgress", isOpening ? 1f : 0f);

            onComplete?.Invoke();
        }

    }
}