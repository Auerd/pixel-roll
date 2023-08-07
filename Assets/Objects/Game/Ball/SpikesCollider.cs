using UnityEngine;
using UnityEngine.Events;

namespace Game.Ball
{
    public sealed class SpikesCollider : MonoBehaviour
    {
        [SerializeField] UnityEvent onSpikeCollision;

        public bool IsAnimationRunning {get; private set;}

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && onSpikeCollision != null)
                onSpikeCollision.Invoke();
        }
    }
}