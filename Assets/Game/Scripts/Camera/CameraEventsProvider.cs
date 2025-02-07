using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Camera
{
    public class CameraEventsProvider : MonoBehaviour, ICameraEventsProvider, ICameraEventsHandler
    {
        private readonly List<ICameraEventsHandler> _handlers = new();
        private HorizontalCameraMovementStrategy _cameraStrategy;
        public PointerEventData LastPointerEventData { get; private set; }

        public void Initialize(HorizontalCameraMovementStrategy cameraStrategy)
        {
            _cameraStrategy = cameraStrategy;
        }

        public void RegisterHandler(ICameraEventsHandler handler)
        {
            _handlers.Add(handler);
        }

        public void UnregisterHandler(ICameraEventsHandler handler)
        {
            _handlers.Remove(handler);
        }
        
#region Events
        public void OnBeginDrag(PointerEventData eventData)
        {
            LastPointerEventData = eventData;
            _cameraStrategy?.ProcessStartDrag(eventData.position);

            if (_handlers.Count > 0)
            {
                _handlers[^1].OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            LastPointerEventData = eventData;
            _cameraStrategy?.ProcessDrag(eventData.position);

            if (_handlers.Count > 0)
            {
                _handlers[^1].OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            LastPointerEventData = eventData;
            _cameraStrategy?.ProcessEndDrag();

            if (_handlers.Count > 0)
            {
                _handlers[^1].OnEndDrag(eventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            LastPointerEventData = eventData;

            if (_handlers.Count > 0)
            {
                _handlers[^1].OnPointerClick(eventData);
            }
        }
        #endregion
    }
}
