using UnityEngine.EventSystems;

namespace Game.Scripts.Camera
{
    public interface ICameraEventsHandler : IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        
    }
}