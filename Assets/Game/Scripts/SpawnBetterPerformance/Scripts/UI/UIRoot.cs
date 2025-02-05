using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform hudLayer;
        [SerializeField] private Transform popupLayer;
        [SerializeField] private Transform overlayLayer;
        [SerializeField] private Transform fullScreenLayer;

        public Transform GetLayer(UILayer layer)
        {
            return layer switch
            {
                UILayer.HUD => hudLayer,
                UILayer.Popup => popupLayer,
                UILayer.Overlay => overlayLayer,
                UILayer.FullScreen => fullScreenLayer,
                _ => fullScreenLayer
            };
        }
    }
}