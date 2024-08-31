using UnityEngine.EventSystems;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.Camera
{
    public interface ICameraEventsProvider
    {
        void RegisterHandler(ICameraEventsHandler handler);
        void UnregisterHandler(ICameraEventsHandler handler);
        PointerEventData LastPointerEventData { get; }
    }
}