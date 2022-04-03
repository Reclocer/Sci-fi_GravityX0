using System.Collections;
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

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //if (_targetPositions.Count > 0)
            //{
            //    MovingToTargetPosition(_targetPositions.Peek(), Time.deltaTime);
            //}
        }

        public void MoveTo(Vector3 newPosition)
        {  
            _rigidbody.useGravity = false;

            transform.DOMove(newPosition, 1).OnComplete(() =>
            {
                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.velocity = Vector3.zero;

                transform.position = newPosition;
                transform.rotation = Quaternion.identity;
            });

            transform.DORotateQuaternion(Quaternion.identity, 1);
            
            //_targetPositions.Enqueue(newPosition);
        }

        private void MovingToTargetPosition(Vector3 targetPosition, float speed)
        {
            //if (Vector3.Distance(transform.position, targetPosition) > 4f * speed)
            //{
            //    transform.position = Vector3.Lerp(transform.position, targetPosition, 2f * speed);
            //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 1f * speed);
            //}
            //else if (Vector3.Distance(transform.position, targetPosition) < 4.5f * speed
            //      && Vector3.Distance(transform.position, targetPosition) > 2.5f * speed)
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.2f * speed);
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 1f * speed);
            //}
            //else if (Vector3.Distance(transform.position, targetPosition) < 3f * speed)
            //{
            //    _targetPositions.Dequeue();

            //    _rigidbody.angularVelocity = Vector3.zero;
            //    _rigidbody.velocity = Vector3.zero;

            //    transform.position = targetPosition;
            //    transform.rotation = Quaternion.identity;
            //}
        }
    }
}
