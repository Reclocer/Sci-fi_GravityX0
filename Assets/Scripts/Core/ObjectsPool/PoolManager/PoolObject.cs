using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Corebin.Core.ObjectsPool
{
    [AddComponentMenu("Pool/PoolObject")]
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}