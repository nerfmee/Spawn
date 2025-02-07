using UnityEngine;

namespace Game.Scripts.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        [SerializeField] private UILayer _layer; 
        public UILayer Layer => _layer; 
        
        public virtual void Initialize(object data)
        {
            Debug.Log($"Window initialized with data: {data}");
        }

        public virtual void PlayOpenAnimation(System.Action onComplete)
        {
            onComplete?.Invoke();
        }

        public virtual void PlayCloseAnimation(System.Action onComplete)
        {
            onComplete?.Invoke();
        }

        public virtual void ShowImmediately()
        {
            gameObject.SetActive(true);
        }

        public virtual void HideImmediately()
        {
            gameObject.SetActive(false);
        }
    }
}
