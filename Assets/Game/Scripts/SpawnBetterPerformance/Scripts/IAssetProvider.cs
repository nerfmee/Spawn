using Project.Game.Scripts.SpawnBetterPerformance.Scripts.Services;
using UnityEngine;

namespace Project.Game.Scripts.SpawnBetterPerformance.Scripts
{
    public interface IAssetProvider : IService
    {
         GameObject Instantiate(string path);
         GameObject Instantiate(string path, Vector3 at);
    }
}