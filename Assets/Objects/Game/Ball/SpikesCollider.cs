using System.Collections;
using UnityEngine;

namespace Game.Ball
{
    public sealed class SpikesCollider : MonoBehaviour
    {
        [SerializeField] Event onSpikeCollision;

        public bool IsAnimationRunning {get; private set;}

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && onSpikeCollision != null)
                onSpikeCollision.Raise();
        }

    }
}