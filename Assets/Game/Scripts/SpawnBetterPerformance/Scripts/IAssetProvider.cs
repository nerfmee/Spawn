using Game.Scripts.SpawnBetterPerformance.Scripts.Factory.Entities;
using Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;

namespace Game.Scripts.SpawnBetterPerformance.Scripts
{
    public interface IAssetProvider : IService
    {
        T LoadEntityPrefab<T>(string path) where T : MonoBehaviour, IEntity;
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}