using System.Collections.Generic;
using UnityEngine;

public sealed class PoolContainer : MonoBehaviour
{
    private readonly Stack<PoolObject> _store = new Stack<PoolObject>(64);

    private GameObject _prefab;

    public string PrefabPath = "UnknownPrefab";

    private bool LoadPrefab()
    {
        _prefab = Resources.Load<GameObject>(PrefabPath);
        if (_prefab == null)
        {
            Debug.LogWarning("Cant load asset " + PrefabPath);
            return false;
        }

#if UNITY_EDITOR
        if (_prefab.GetComponent<PoolObject>() != null)
        {
            Debug.LogWarning("PoolObject cant be used on prefabs");
            _prefab = null;
            UnityEditor.EditorApplication.isPaused = true;
            return false;
        }
#endif
        return true;
    }

    public PoolObject Get()
    {
        if (_prefab == null)
        {
            if (!LoadPrefab())
            {
                return null;
            }
        }

        PoolObject obj;
        if (_store.Count > 0)
        {
            obj = _store.Pop();
        }
        else
        {
            var go = Instantiate<GameObject>(_prefab);
            obj = go.AddComponent<PoolObject>();
            obj.SetPool(this);
        }
        obj.SetActive(false);
        return obj;
    }

    public void Recycle(PoolObject obj)
    {
        if (obj != null && obj.Pool == this)
        {
            obj.SetActive(false);
            if (!_store.Contains(obj))
            {
                _store.Push(obj);
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Invalid obj to recycle", obj);
#endif
        }
    }
}