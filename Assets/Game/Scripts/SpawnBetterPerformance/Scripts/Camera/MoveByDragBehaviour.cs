using System.Collections;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
    public class MoveByDragBehaviour : IHorizontalCameraMovementBehaviour
    {
        private readonly DragCameraMovementData _movementData;
        private const float MOVE_SPEED_LOWER_LIMIT = 0.05f;
        
        private readonly UnityEngine.Camera _camera;
        private readonly Transform _cameraHolder;
        private readonly Transform _xRotator;
        
        private Vector3 _dragVelocity;
        private Vector3 _startDragPosition;
        private Vector3 _objectStartPosition;
        private Vector3? _targetLocalPosition;
        
        private Plane _plane;
        private readonly MapLimitsForCameraData _mapLimitsForCameraData;
        private Vector3 _worldMapPoint;
        private readonly MonoBehaviour _coroutineObject;
        private Coroutine _mapLimitCoroutine;
        
        public MoveByDragBehaviour(CameraObjectsData objectsData, DragCameraMovementData movementData, MapLimitsForCameraData mapLimitsForCameraData, MonoBehaviour gameObject)
        {
            _movementData = movementData;
            _mapLimitsForCameraData = mapLimitsForCameraData;
            _camera = objectsData.Camera;
            _cameraHolder = objectsData.CameraHolder;
            _xRotator = objectsData.XRotator;
            _plane = new Plane(objectsData._plane.up, objectsData._plane.position);
            _coroutineObject = gameObject;
        }

        public void ProcessStartDrag(Vector2 screenPosition)
        {
            _startDragPosition = ConvertFromScreenToPlaneInCameraHolderSpace(screenPosition);
            _objectStartPosition = _cameraHolder.localPosition;
            _targetLocalPosition = _objectStartPosition;
            
            if (_mapLimitCoroutine != null)
            {
                _coroutineObject.StopCoroutine(_mapLimitCoroutine);
            }
        }

        public void ProcessDrag(Vector2 screenPosition)
        {
            Vector3 newPos = ConvertFromScreenToPlaneInCameraHolderSpace(screenPosition);
            Vector3 delta = newPos - _startDragPosition;
            //Speed up z-axis movement to make it visually look equal to x-axis movement
            delta.y *= 1f / Mathf.Sin(_xRotator.localEulerAngles.x * Mathf.Deg2Rad);
            _targetLocalPosition = _objectStartPosition - new Vector3(delta.x,0, delta.y);
            _targetLocalPosition = CalculateLimitPosition(false);
        }
        
        public void ProcessEndDrag()
        { 
            ReturnToCameraLimitsRoutine();
        }

        public void Update()
        {
            TryMoveCameraToTargetPosition();
            ProcessInertiaMovement(Time.deltaTime);
        }

        public void Reset()
        {
            _targetLocalPosition = null;
            _dragVelocity = Vector3.zero;
        }
        
        private void ReturnToCameraLimitsRoutine()
        {
            if (_mapLimitCoroutine == null)
            {
                _mapLimitCoroutine = _coroutineObject.StartCoroutine(ReturnToCameraLimits());
            }
            else
            {
                _coroutineObject.StopCoroutine(_mapLimitCoroutine);
                _mapLimitCoroutine = _coroutineObject.StartCoroutine(ReturnToCameraLimits());
            }
        }
        
        private Vector3 ConvertFromScreenToPlaneInCameraHolderSpace(Vector2 screenPosition)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);
            Vector3 worldPoint = RayIntersectionWithPlane(ray);
            return _cameraHolder.InverseTransformPoint(worldPoint);
        }

        private Vector3 RayIntersectionWithPlane(Ray ray)
        {
            if (!_plane.Raycast(ray, out var enterDistance))
            {
                Debug.LogError("Invalid camera position - no intersection with plane");
                return Vector2.zero;
            }

            return ray.GetPoint(enterDistance);
        }

        private void TryMoveCameraToTargetPosition()
        {
            if (!_targetLocalPosition.HasValue)
            {
                return;
            }
            
            _cameraHolder.localPosition = Vector3.SmoothDamp(
                _cameraHolder.localPosition, 
                _targetLocalPosition.Value, 
                ref _dragVelocity, 
                _movementData.SmoothTime);

            if (_dragVelocity.sqrMagnitude < MOVE_SPEED_LOWER_LIMIT)
            {
                _dragVelocity = Vector3.zero;
            }
        }

        private Vector3? CalculateLimitPosition(bool isOffset)
        {
            var bottomLimit = _mapLimitsForCameraData.BottomLimit;
            var upperLimit = _mapLimitsForCameraData.UpperLimit;
            var offset = _mapLimitsForCameraData.OffsetToReturnLimit;

            if (!_targetLocalPosition.HasValue)
            {
                return null;
            }
            
            if (!isOffset)
            {
                offset = 0;
            }

            var osX = Mathf.Clamp(_targetLocalPosition.Value.x, bottomLimit.x + offset, bottomLimit.y - offset);
            var osY = Mathf.Clamp(_targetLocalPosition.Value.z, upperLimit.x + offset, upperLimit.y - offset);
            
            return new Vector3(osX,1, osY);;
        }

        private IEnumerator ReturnToCameraLimits()
        {
            Vector3? endPosition = CalculateLimitPosition(true);
            if (!endPosition.HasValue)
            {
                _targetLocalPosition = null;
                yield break;
            }
            
            var startPosition = _targetLocalPosition;
           
            var returnTime = _mapLimitsForCameraData.TimeToReturn;

            if (!startPosition.HasValue)
            {
                _targetLocalPosition = null;
                yield break;
            }
            
            var test = startPosition - endPosition;
            if (test.Value == Vector3.zero)
            {
                _targetLocalPosition = null;
                yield break;
            }

            for (float t = 0f; t < returnTime; t += Time.deltaTime)
            {
                _targetLocalPosition = Vector3.Lerp(startPosition.Value, endPosition.Value, t/returnTime );
                yield return null;
            }

            _targetLocalPosition = null;
        }
        
        private void ProcessInertiaMovement(float deltaTime)
        {
            if (_targetLocalPosition.HasValue)
            {
                return;
            }

            _dragVelocity *= 1 - _movementData.InertiaCoefficient * deltaTime;
            if (_dragVelocity.sqrMagnitude <= MOVE_SPEED_LOWER_LIMIT)
            {
                _dragVelocity = Vector3.zero;
            }

            _cameraHolder.localPosition += _dragVelocity * deltaTime;
        }
        
    }
}