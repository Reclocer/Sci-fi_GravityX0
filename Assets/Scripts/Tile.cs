using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Corebin.GravityX0
{
    [RequireComponent(typeof(Rigidbody))]
    public class Tile : MonoBehaviour
    {
        public Fill Fill = Fill.Iks;
        private Rigidbody _rigidbody;
        private Queue<Vector3> _targetPositions = new Queue<Vector3>(8);
        private bool _isMoving = false;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void MoveTo(Vector3 newPosition, Action callBack = null)
        {
            if (_isMoving)
                return;

            _isMoving = true;
            _rigidbody.useGravity = false;

            transform.DOMove(newPosition, 1).OnComplete(() =>
            {
                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.velocity = Vector3.zero;

                transform.position = newPosition;
                transform.rotation = Quaternion.identity;

                _isMoving = false;
                callBack.Invoke();
            });

            transform.DORotateQuaternion(Quaternion.identity, 1);
            
            //_targetPositions.Enqueue(newPosition);
        }

        //private void MovingToTargetPosition(Vector3 targetPosition, float speed)
        //{
        //    _targetPositions.Dequeue(newPosition);
        //}
    }
}
