using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "UI/WindowData", order = 1)]
    public class WindowData : ScriptableObject
    {
        public string windowName;
        public GameObject windowPrefab;
    }
}