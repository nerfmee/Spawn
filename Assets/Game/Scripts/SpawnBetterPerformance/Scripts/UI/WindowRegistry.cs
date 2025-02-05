using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    [CreateAssetMenu(fileName = "WindowRegistry", menuName = "UI/WindowRegistry", order = 2)]
    public class WindowRegistry : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> windowPrefabs = new();

        private readonly Dictionary<Type, GameObject> _registeredWindows = new();

        public void UpdateWindowPrefabs(List<GameObject> newPrefabs)
        {
            windowPrefabs = newPrefabs;

            _registeredWindows.Clear();
            foreach (var prefab in windowPrefabs)
            {
                if (prefab.TryGetComponent<UIElement>(out var controller))
                {
                    _registeredWindows[controller.GetType()] = prefab;
                }
                else
                {
                    Debug.LogWarning($"Prefab {prefab.name} does not contain a WindowController component.");
                }
            }
        }

        public GameObject GetWindowPrefab(Type windowType)
        {
            return _registeredWindows.TryGetValue(windowType, out var prefab) ? prefab : null;
        }

        public void RegisterWindowsForManager(WindowManager manager)
        {
            foreach (var prefab in windowPrefabs)
            {
                if (prefab.TryGetComponent<UIElement>(out var uiElement))
                {
                    var type = uiElement.GetType();
                    manager.RegisterWindow(type, uiElement);
                }
                else
                {
                    Debug.LogWarning($"Prefab {prefab.name} does not contain a UIElement component.");
                }
            }
        }
        
    }
}