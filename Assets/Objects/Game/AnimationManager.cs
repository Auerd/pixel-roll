using System.Collections;
using UnityEngine;

namespace Game
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] Event trigger, animationEnded = null;
        [SerializeField] AnimationSettings settings;
        [System.Serializable] private struct AnimationSettings
        {
            [Min(1)] public uint cycles;
            public float cycleLength;
            public Material[] states;
        }

        private bool isAnimating;
        private new Renderer renderer;
        private AnimationManager[] thisObjectManagers;
        private Coroutine coroutine;
        private float pause;


        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            trigger.Subscribe(Animate);
            settings.cycleLength = Mathf.Abs(settings.cycleLength);
            thisObjectManagers = GetComponents<AnimationManager>();
            pause = settings.cycleLength / settings.states.Length;
        }

        private void Animate()
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
        }
    }
}