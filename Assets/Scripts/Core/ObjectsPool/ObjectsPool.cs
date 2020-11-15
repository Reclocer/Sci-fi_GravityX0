using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corebin.Core.ObjectsPool
{
    [Serializable]
    public class ObjectsPool<T> 
    {
        public List<T> Pool => _pool;
        private List<T> _pool;

        public ObjectsPool()
        {
            _pool = new List<T>(64);
        }

        public ObjectsPool(int objectsCount)
        {
            _pool = new List<T>(objectsCount);
        }        

        public void Add(T @object)
        {
            _pool.Add(@object);
        }

        public void Remove(T @object)
        {
            _pool.Remove(@object);
        }

        public int RemoveAll(Predicate<T> condition)
        {
            return _pool.RemoveAll(condition);
        } 
    }
}
