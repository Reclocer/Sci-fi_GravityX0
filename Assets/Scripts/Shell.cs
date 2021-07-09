using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corebin.GravityX0
{
    public class Shell : MonoBehaviour
    {
        [SerializeField] private float _explosionForce = 1;
        [SerializeField] private float _explosionRadius = 1;

        [SerializeField] private TilesManager _tilesManager;

        private void Start()
        {
            Invoke(nameof(Bang), 0.4f);
        }

        private void Bang()
        {
            Rigidbody[] rigidbodies = _tilesManager.TilesRigidbodies;

            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.useGravity = true;

                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 0.1f, ForceMode.Impulse);
            }
        }
    }
}
