using UnityEngine;

namespace Game.Ball
{
    public sealed class SpikesCollider : MonoBehaviour
    {
        [SerializeField]
        Event onSpikeCollision;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && onSpikeCollision != null)
                onSpikeCollision.Raise();
        }
    }
}