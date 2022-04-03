using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if(_targetPositions.Count > 0)
            {
                MovingToTargetPosition(_targetPositions.Peek());
            }
        }

        public void MoveTo(Vector3 newPosition)
        {
            //transform.position = newPosition;  
            _targetPositions.Enqueue(newPosition);

            _rigidbody.useGravity = false;
            //_rigidbody.angularVelocity = Vector3.zero;
            //_rigidbody.velocity = Vector3.zero;
            //transform.rotation = Quaternion.identity;
        }

        private void MovingToTargetPosition(Vector3 targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.02f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.01f);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                _targetPositions.Dequeue();

                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.velocity = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
        }        
    }
}
