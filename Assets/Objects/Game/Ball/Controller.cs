using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Time;

namespace Game.Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Controller : MonoBehaviour
    {
        [SerializeField] float acceleration = 0.5f;
        [SerializeField] new Camera camera;
        [SerializeField] Event OnSpikeCollision;
        [SerializeField] Canvas canvas;

        private Rigidbody2D rb;
        private bool dead;


        #region Singleton

        private Controller () { }
        public static Controller Instance { get; private set; }

        #endregion

        private void Awake()
        {
            Instance = this;
            OnSpikeCollision.Subscribe(StopAll);
            rb = GetComponent<Rigidbody2D>();
            dead = false;
        }

        void FixedUpdate()
        {
            Vector2 force;
#if UNITY_STANDALONE
            force = new(
                GetAxisRaw("Horizontal")
                * acceleration 
                * fixedDeltaTime
                * camera.orthographicSize
                * transform.localScale.x, 0);
#elif UNITY_ANDROID || UNITY_IOS
            if (touchCount > 0)
            {
                force = new(acceleration * fixedDeltaTime * camera.orthographicSize * transform.localScale.x, 0);
                if (GetTouch(0).position.x <= camera.pixelWidth / 2)
                    force = -force;
            }
            else
                force = Vector2.zero;
#endif
            if (!dead)
            {
                rb.AddForce(force);
            }
        }

        private void StopAll()
        {
            dead = true;
            rb.velocity = -GameManagement.GameManager.Speed;
            rb.isKinematic = true;
            rb.gravityScale = 0;
        }
    }
}