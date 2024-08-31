using System;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
    [Serializable]
    public class DragCameraMovementData
    {
        public float SmoothTime = 0.5f;
        public float InertiaCoefficient = 1.4f;
    }
}