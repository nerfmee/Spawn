using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    public abstract class WindowController : MonoBehaviour
    {
        public virtual void Initialize(object data)
        {
            Debug.Log($"Window initialized with data: {data}");
        }

        public virtual void PlayOpenAnimation(System.Action onComplete)
        {
            Debug.Log("Playing open animation...");
            onComplete?.Invoke();
        }

        public virtual void PlayCloseAnimation(System.Action onComplete)
        {
            Debug.Log("Playing close animation...");
            onComplete?.Invoke();
        }

        public void CloseWindowImmediately()
        {
            gameObject.SetActive(false);
        }
    }
}
