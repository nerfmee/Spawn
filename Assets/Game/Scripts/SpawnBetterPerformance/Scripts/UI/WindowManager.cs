using System;
using System.Collections.Generic;
using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.SpawnBetterPerformance.Scripts.UI
{
    public class WindowManager : IService
    {
        private readonly Transform _uiRoot;
        private readonly Dictionary<Type, GameObject> _registeredWindows = new();
        private readonly Dictionary<Type, WindowController> _openWindows = new();

        public WindowManager(Transform uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public void RegisterWindow(Type windowType, GameObject prefab)
        {
            if (!_registeredWindows.ContainsKey(windowType))
            {
                _registeredWindows[windowType] = prefab;
            }
            else
            {
                Debug.LogWarning($"Window type {windowType.Name} is already registered.");
            }
        }

        public T OpenWindow<T>(object data = null, Action onOpen = null) where T : WindowController
        {
            Type windowType = typeof(T);

            if (_openWindows.ContainsKey(windowType))
            {
                Debug.LogWarning($"Window {windowType.Name} is already open.");
                return null;
            }

            if (!_registeredWindows.TryGetValue(windowType, out var prefab))
            {
                Debug.LogError($"No prefab registered for window {windowType.Name}. Make sure it's registered.");
                return null;
            }

            var windowObject = Object.Instantiate(prefab, _uiRoot);
            var controller = windowObject.GetComponent<T>();

            if (controller == null)
            {
                Debug.LogError($"The prefab {prefab.name} does not have a component of type {typeof(T)}.");
                Object.Destroy(windowObject);
                return null;
            }

            controller.Initialize(data);

            controller.PlayOpenAnimation(() =>
            {
                onOpen?.Invoke();
            });

            _openWindows[windowType] = controller;
            return controller;
        }

        public void CloseWindow<T>(Action onClose = null) where T : WindowController
        {
            Type windowType = typeof(T);

            if (!_openWindows.TryGetValue(windowType, out var controller))
            {
                Debug.LogWarning($"Window {windowType.Name} is not open.");
                return;
            }

            controller.PlayCloseAnimation(() =>
            {
                _openWindows.Remove(windowType);
                Object.Destroy(controller.gameObject);
                onClose?.Invoke();
            });
        }

        public bool IsWindowOpen<T>() where T : WindowController
        {
            return _openWindows.ContainsKey(typeof(T));
        }
    }
}
