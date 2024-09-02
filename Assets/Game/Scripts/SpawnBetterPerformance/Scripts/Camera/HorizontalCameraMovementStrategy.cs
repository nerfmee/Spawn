using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
    public class HorizontalCameraMovementStrategy
    {
        private readonly CameraObjectsData _moveToTargetData;
        private readonly MoveByDragBehaviour _moveByDragBehaviour;

        private IHorizontalCameraMovementBehaviour _currentBehaviour;
        private bool _isEnable;

        public HorizontalCameraMovementStrategy(
            CameraObjectsData objectsData,
            DragCameraMovementData dragCameraMovementData,
            MapLimitsForCameraData mapLimitsForCameraData,
            MonoBehaviour gameObject)
        {
            _moveByDragBehaviour = new MoveByDragBehaviour(objectsData, dragCameraMovementData, mapLimitsForCameraData, gameObject);
            _currentBehaviour = _moveByDragBehaviour;
            _isEnable = true;
        }

        public void ProcessStartDrag(Vector2 screenPosition)
        {
            if (!_isEnable)
            {
                return;
            }

            SwitchToBehaviour(_moveByDragBehaviour);
            _moveByDragBehaviour.ProcessStartDrag(screenPosition);
        }

        public void ProcessDrag(Vector2 screenPosition)
        {
            if (!_isEnable)
            {
                return;
            }

            _moveByDragBehaviour.ProcessDrag(screenPosition);
        }

        public void ProcessEndDrag()
        {
            if (!_isEnable)
            {
                return;
            }

            _moveByDragBehaviour.ProcessEndDrag();
        }

        public void Update()
        {
            _currentBehaviour.Update();
        }

        public void SetControlEnableState(bool value)
        {
            _isEnable = value;
        }

        private void SwitchToBehaviour(IHorizontalCameraMovementBehaviour newBehaviour)
        {
            if (_currentBehaviour == newBehaviour)
            {
                return;
            }

            _currentBehaviour.Reset();
            _currentBehaviour = newBehaviour;
        }
    }
}
