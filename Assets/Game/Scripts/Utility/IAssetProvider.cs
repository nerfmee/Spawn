using Game.Scripts.Factory.Entities;
using Game.Scripts.Services;
using UnityEngine;

namespace Game.Scripts
{
    public interface IAssetProvider : IService
    {
        T LoadEntityPrefab<T>(string path) where T : MonoBehaviour, IEntity;
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}