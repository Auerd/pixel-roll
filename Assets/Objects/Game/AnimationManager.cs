using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] UnityEvent animationEnded;
        [SerializeField] AnimationSettings settings;
        [System.Serializable] private struct AnimationSettings
        {
            [Min(1)] public uint cycles;
            public float cycleLength;
            public Material[] states;
        }
        [SerializeField, TextArea(1, 5)] string description;

        private bool isAnimating;
        private new Renderer renderer;
        private AnimationManager[] thisObjectManagers;
        private Coroutine coroutine;
        private float pause;


        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            settings.cycleLength = Mathf.Abs(settings.cycleLength);
            thisObjectManagers = GetComponents<AnimationManager>();
            pause = settings.cycleLength / settings.states.Length;
        }

        public void Animate()
        {
            if (!isAnimating)
            {
                foreach (var manager in thisObjectManagers)
                    manager.StopAnimating();
                coroutine = StartCoroutine(AnimateCoroutine());
            }
        }

        private void StopAnimating()
        {
            if (isAnimating)
                StopCoroutine(coroutine);
        }

        private IEnumerator AnimateCoroutine()
        {
            isAnimating = true;
            for (uint cycle = 0; cycle < settings.cycles; cycle++)
            {
                foreach(Material state in settings.states)
                {
                    renderer.material = state;
                    yield return new WaitForSeconds(pause);
                }
            }
            isAnimating = false;
            animationEnded?.Invoke();
        }
    }
}