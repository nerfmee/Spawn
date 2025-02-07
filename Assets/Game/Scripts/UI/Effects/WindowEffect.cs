using UnityEngine;

namespace Game.Scripts.UI.Effects
{
    public class WindowEffect : MonoBehaviour
    {
        public Material dissolveMaterial;
        public float duration = 1.0f;

        private float _dissolveProgress = 0f;
        private bool _isOpening = true;

        public void OpenWindow()
        {
            _isOpening = true;
        }

        public void CloseWindow()
        {
            _isOpening = false;
        }
    }
}