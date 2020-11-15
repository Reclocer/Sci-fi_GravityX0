using UnityEngine;

namespace Corebin.Core
{
    public abstract class SingletonBase<T> : MonoBehaviour
    {
        [SerializeField] private bool _dontDestroyOnLoad = false;

        public static T Instance { get; protected set; }
        
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetInstance();
            }
            else if (!_dontDestroyOnLoad)
            {
                Destroy(this);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        protected abstract T GetInstance();
    }
}
