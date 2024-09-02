using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts
{
    public class CustomPool <T> where T: MonoBehaviour
    {
        private readonly T _prefab;
        private readonly List<T> _objects;
        private readonly Transform _container;
        private const int MAX_OBJECTS_COUNT = 1000;
        private int _objectsCount;
        
        public CustomPool(T prefab, int objectsCount, Transform container)
        {
            _prefab = prefab;
            _objects = new List<T>();
            _container = container;
            _objectsCount = objectsCount;
            _objects.Capacity = MAX_OBJECTS_COUNT;
            CreatePool();
        }

        private void CreatePool()
        {
            ValidateObjectsCount();
            
            for (int i = 0; i < _objectsCount; i++)
            {
                var obj = Object.Instantiate(_prefab, _container);
                obj.gameObject.SetActive(false);
                _objects.Add(obj);
            }
        }

        private void ValidateObjectsCount()
        {
            //TODO нужно доделать чтобы этот метод возвращал что-то
            if (_objectsCount > MAX_OBJECTS_COUNT)
            {
                _objectsCount = MAX_OBJECTS_COUNT;
            }
        }
        
        public T Get()
        {
            var obj = FindUnusedObject();
            
            if (obj == null)
            {
                obj = Create();
            }
            obj.gameObject.SetActive(true);
            return obj;
        }

        private T FindUnusedObject()
        {
            foreach (var element in _objects)
            {
                return element.gameObject.activeInHierarchy ? null : element;
            }
            return null;
        }

        private T Create()
        {
            var obj = Object.Instantiate(_prefab, _container);
            _objects.Add(obj);
            return obj;
        }
    }
}