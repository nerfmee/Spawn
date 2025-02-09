using Game.Scripts.Services;
using Game.Scripts.Spawn.Entities;
using UnityEngine;

namespace Game.Scripts.Utility
{
    public interface IAssetProvider : IService
    {
        T LoadEntityPrefab<T>(string path) where T : MonoBehaviour, IEntity;
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}