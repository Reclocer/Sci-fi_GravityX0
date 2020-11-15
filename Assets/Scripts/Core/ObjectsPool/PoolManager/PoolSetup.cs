using UnityEngine;
using System.Collections;

namespace Corebin.Core.ObjectsPool
{
    [AddComponentMenu("Pool/PoolSetup", 0)]
    public class PoolSetup : MonoBehaviour
    {            
        [SerializeField] private PoolManager.PoolPart[] _pools;        

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            PoolManager.Initialize(_pools);
        }

        private void OnValidate()
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i].name = _pools[i].prefab.name;
            }
        }
    }
}