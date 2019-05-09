using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class TriggerCollisionDetector : MonoBehaviour
    {
        public event Action<TriggerCollisionDetector, Collider> OnTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (OnTrigger != null)
            {
                OnTrigger.Invoke(this, other);
            }
        }
    }
}