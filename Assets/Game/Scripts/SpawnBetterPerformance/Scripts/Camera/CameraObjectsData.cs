using System;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{    
    [Serializable]
    public struct CameraObjectsData
    {
        [SerializeField]
        public Transform _townPlane;
        
        [SerializeField]
        private UnityEngine.Camera _camera;

        [SerializeField]
        private Transform _cameraHolder;

        [SerializeField]
        private Transform _xRotator;
        
        public UnityEngine.Camera Camera => _camera;
        public Transform CameraHolder => _cameraHolder;
        public Transform XRotator => _xRotator;
    }
}