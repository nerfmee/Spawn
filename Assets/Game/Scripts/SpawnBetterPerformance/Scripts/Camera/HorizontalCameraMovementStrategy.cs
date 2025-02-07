using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
    public class HorizontalCameraMovementStrategy : IService
    {
        private readonly CameraObjectsData _moveToTargetData;
        private readonly MoveByDragBehaviour _moveByDragBehaviour;
        private readonly FollowPlayerBehaviour _followPlayerBehaviour;

        private IHorizontalCameraMovementBehaviour _currentBehaviour;
        private bool _isEnable;
        
        private float _timeSinceLastInput;
        private readonly float _followDelay = 2f;

        public HorizontalCameraMovementStrategy(
            CameraObjectsData objectsData,
            DragCameraMovementData dragCameraMovementData,
            MapLimitsForCameraData mapLimitsForCameraData,
            MonoBehaviour gameObject)
        {
            _moveByDragBehaviour = new MoveByDragBehaviour(objectsData, dragCameraMovementData, mapLimitsForCameraData, gameObject);
            _followPlayerBehaviour = new FollowPlayerBehaviour(objectsData,  5);
            _currentBehaviour = _moveByDragBehaviour;
            _isEnable = true;
        }

        public void ProcessStartDrag(Vector2 screenPosition)
        {
            if (!_isEnable)
            {
                return;
            }
            
            _timeSinceLastInput = 0f;
            SwitchToBehaviour(_moveByDragBehaviour);
            _moveByDragBehaviour.ProcessStartDrag(screenPosition);
        }

        public void ProcessDrag(Vector2 screenPosition)
        {
            if (!_isEnable)
            {
                return;
            }
            
            _timeSinceLastInput = 0f;
            _moveByDragBehaviour.ProcessDrag(screenPosition);
        }

        public void ProcessEndDrag()
        {
            if (!_isEnable)
            {
                return;
            }

            _timeSinceLastInput = 0f;
            _moveByDragBehaviour.ProcessEndDrag();
        }

        public void Update()
        {
            _timeSinceLastInput += Time.deltaTime;
            if (_timeSinceLastInput >= _followDelay)
            {
                SwitchToBehaviour(_followPlayerBehaviour);
            }
            
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
        
        public void SetFollowTarget(Transform newTarget)
        {
            _followPlayerBehaviour.SetTarget(newTarget);
        }
    }
}
