using System.Collections;
using UnityEngine;

namespace Game
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] Event trigger, animtionEnded = null;
        [SerializeField] AnimationSettings settings;
        [System.Serializable] private struct AnimationSettings
        {
            [Min(1)] public uint cycles;
            public float stateLength;
            public Material[] states;
        }

        private bool isAnimating;
        private new Renderer renderer;
        private AnimationManager[] thisObjectManagers;
        private Coroutine coroutine;


        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            trigger.Subscribe(Animate);
            settings.stateLength = Mathf.Abs(settings.stateLength);
            thisObjectManagers = GetComponents<AnimationManager>();
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
            for(uint cycle = 0; cycle < settings.cycles; cycle++)
            {
                foreach(Material state in settings.states)
                {
                    renderer.material = state;
                    yield return new WaitForSeconds(settings.stateLength);
                }
            }
            isAnimating = false;
        }
    }
}