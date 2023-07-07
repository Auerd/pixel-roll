using UnityEngine;
using Game.Frame.BonusSystem;

namespace Game.Ball
{
    public sealed class BonusCollider : MonoBehaviour
    {
        
        public Bonus Bonus { get; private set; }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Bonus = collision.gameObject.GetComponent<Bonus>();

        }
    }
}