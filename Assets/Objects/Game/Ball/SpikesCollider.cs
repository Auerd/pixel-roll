using System.Collections;
using UnityEngine;

namespace Game.Ball
{
    public sealed class SpikesCollider : MonoBehaviour
    {
        [SerializeField] Event onSpikeCollision;
        [SerializeField] float animationLength;
        [SerializeField] Material[] statesOfTransparency;
        [SerializeField] Material standart;

        private new Renderer renderer;
        public bool IsAnimationRunning {get; private set;}


        private void Awake()
        {
            onSpikeCollision.Subscribe(Death);
            renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            renderer.material = standart;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && onSpikeCollision != null)
                onSpikeCollision.Raise();
        }

        private void Death()
        {
            if(!IsAnimationRunning)
                StartCoroutine(Death(animationLength));
        }

        private IEnumerator Death(float animationLength)
        {
            IsAnimationRunning = true;
            float pause = animationLength / (statesOfTransparency.Length);
            foreach(var state in statesOfTransparency)
            {
                yield return new WaitForSeconds(pause);
                renderer.material = state;
            }
            transform.gameObject.SetActive(false);
            IsAnimationRunning = false;
        }
    }
}