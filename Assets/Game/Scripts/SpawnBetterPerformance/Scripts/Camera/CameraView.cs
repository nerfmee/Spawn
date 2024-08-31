using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
   public class CameraView : MonoBehaviour, ICameraEventsHandler
    {
        [SerializeField]
        private PhysicsRaycaster _raycaster;
        [SerializeField]
        private CameraObjectsData _objectsData;
        [SerializeField]
        private DragCameraMovementData _dragMovementData;
        [SerializeField]
        private CameraEventsProvider cameraEventsProvider;

        [Header("Limits")]
        [SerializeField]
        private MapLimitsForCameraData _mapLimitsForCameraData;
        
        private readonly List<RaycastResult> _physicsRaycastResults = new();
        private float _lastFingersDistance;
        private HorizontalCameraMovementStrategy _movementStrategy;

        private void Awake()
        {
            Init();
        }

        private void Init()
        { 
            _movementStrategy = new HorizontalCameraMovementStrategy(_objectsData, _dragMovementData, _mapLimitsForCameraData, this);
            cameraEventsProvider.RegisterHandler(this);
        }

        private void Update()
        {
            _movementStrategy.Update();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _movementStrategy.ProcessStartDrag(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _movementStrategy.ProcessDrag(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _movementStrategy.ProcessEndDrag();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.dragging)
            {
                return;
            }

            _physicsRaycastResults.Clear();

            _raycaster.Raycast(eventData, _physicsRaycastResults);
        }

        private void OnDestroy()
        {
            cameraEventsProvider.UnregisterHandler(this);
        }
    }
}
