using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corebin.GravityX0
{   
    [RequireComponent(typeof(Rigidbody))]
    public class Tile : MonoBehaviour
    {
        public Fill Fill = Fill.Iks;

        public void MoveTo(Vector3 newPosition)
        {
            transform.position = newPosition;            
        }
    }
}
