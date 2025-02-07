using UnityEngine;

namespace Game.Scripts.Camera
{
    public class FollowPlayerBehaviour: IHorizontalCameraMovementBehaviour
    {
        private readonly CameraObjectsData _cameraData;
        private Transform _playerTransform;
        private readonly float _followSpeed;
        private readonly float _smoothTime = 0.3f;
        private Vector3 _velocity = Vector3.zero;
        
        public FollowPlayerBehaviour(CameraObjectsData cameraData, float followSpeed)
        {
            _cameraData = cameraData;
            _followSpeed = followSpeed;
        }
        
        public void SetTarget(Transform newTarget)
        {
            _playerTransform = newTarget;
        }
        
        public void Update()
        {
             Vector3 targetPosition = _cameraData.CameraHolder.localPosition;
            targetPosition.x = Mathf.SmoothDamp(
                _cameraData.CameraHolder.localPosition.x,
                _playerTransform.localPosition.x,
                ref _velocity.x,
                _smoothTime,
                _followSpeed
            );

            _cameraData.CameraHolder.localPosition = targetPosition;
        }
        
        public void Reset()
        { }
    }
}