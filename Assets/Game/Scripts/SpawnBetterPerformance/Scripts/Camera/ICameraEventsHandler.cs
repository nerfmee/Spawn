using UnityEngine.EventSystems;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
    public interface ICameraEventsHandler : IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        
    }
}