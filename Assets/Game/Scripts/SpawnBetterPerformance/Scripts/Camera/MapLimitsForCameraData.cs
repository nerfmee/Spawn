using System;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
    [Serializable]
    public class MapLimitsForCameraData
    {
        public Vector2 UpperLimit;
        public Vector2 BottomLimit;
        [Range(0,100)] 
        public float OffsetToReturnLimit;
        [Range(0,10)] 
        public float TimeToReturn;

        public void ApplyValues(MapLimitsForCameraData otherLimits)
        {
            UpperLimit = otherLimits.UpperLimit;
            BottomLimit = otherLimits.BottomLimit;
            OffsetToReturnLimit = otherLimits.OffsetToReturnLimit;
            TimeToReturn = otherLimits.TimeToReturn;
        }
    }
}