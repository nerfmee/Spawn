using System.Collections;
using Game.Scripts.Services;
using Game.Scripts.UI.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class SettingsWindowView : UIElement
    {
        private static readonly int DissolveProgress = Shader.PropertyToID("_DissolveProgress");
        [SerializeField] private Button settingsButton;
        [SerializeField] private WindowEffect windowEffect;
        private WindowManager _windowManager;

        public override void Initialize(object data)
        {
            base.Initialize(data);
            Debug.Log("Initializing settings window...");
            settingsButton.onClick.AddListener(CloseWindow);
        }

        public override void PlayOpenAnimation(System.Action onComplete)
        {
            StartCoroutine(AnimateDissolve(true, () =>
            {
                base.PlayOpenAnimation(onComplete);
                onComplete?.Invoke();
            }));
        }

        public override void PlayCloseAnimation(System.Action onComplete)
        {
            StartCoroutine(AnimateDissolve(false, () =>
            {
                base.PlayCloseAnimation(onComplete);
                onComplete?.Invoke();
            }));
        }

        private void CloseWindow()
        {
            _windowManager = AllServices.Container.Single<WindowManager>();
            _windowManager.CloseWindow<SettingsWindowView>();
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
                windowEffect.dissolveMaterial.SetFloat(DissolveProgress, dissolveValue);
                yield return null;
            }

            windowEffect.dissolveMaterial.SetFloat(DissolveProgress, isOpening ? 1f : 0f);

            onComplete?.Invoke();
        }
    }
}