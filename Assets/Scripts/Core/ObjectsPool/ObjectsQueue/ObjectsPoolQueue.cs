using System;
using System.Collections.Generic;
using UnityEngine;

namespace Corebin.Core.ObjectsPool.ObjectsQueue
{
    [Serializable]
    public sealed class ObjectsPoolQueue<T>
    {        
        public Queue<T>[] Pools => _pools;
        private Queue<T>[] _pools;

        public int ObjectsCount => _objectsCount;
        private int _objectsCount = 64;

        #region Constructors
        public ObjectsPoolQueue()
        {
            _pools = new Queue<T>[2];

            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i] = new Queue<T>(_objectsCount);
            }
        }

        public ObjectsPoolQueue(int objectsCount)
        {
            _pools = new Queue<T>[2];

            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i] = new Queue<T>(objectsCount);
            }

            _objectsCount = objectsCount;
        }

        public ObjectsPoolQueue(int objectsCount, int poolsCount)
        {
            _pools = new Queue<T>[poolsCount];

            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i] = new Queue<T>(objectsCount);
            }

            _objectsCount = objectsCount;
        }
        #endregion Constructors

        #region Methods
        public void Enqueue(T @object)
        {
            _pools[0].Enqueue(@object);
        }

        public void Enqueue(T @object, int order)
        {
            if (order >= _pools.Length)
            {
                EnqueueLast(@object);
            }
            else if (order <= 0)
            {
                Enqueue(@object);
            }
            else
            {
                _pools[order].Enqueue(@object);
            }
        }

        public void EnqueueLast(T @object)
        {
            int n = _pools.Length;
            _pools[n].Enqueue(@object);
        }

        public T Peek()
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                for (int j = 0; j < _pools[i].Count; j++)
                {
                    T @object = _pools[i].Peek();
                    return @object;
                }
            }

            return default;
        }

        public T Dequeue()
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                for (int j = 0; j < _pools[i].Count; j++)
                {
                    T @object = _pools[i].Dequeue();
                    return @object;
                }
            }

            return default;
        }

        #endregion Methods
    }
}
