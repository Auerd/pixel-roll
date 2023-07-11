using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Time;

namespace Game.Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Controller : MonoBehaviour
    {
        [SerializeField] private TypeOfMoving typeOfMoving;
        [SerializeField] private float acceleration = 0.5f;
        [SerializeField] private new Camera camera;
        [SerializeField] private Event OnSpikeCollision;

        private enum TypeOfMoving
        {
            Force,
            Retro,
        }

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
        }

        void Start()
        {

        }

        void FixedUpdate()
        {
            Vector2 force;
#if UNITY_STANDALONE
            force = new(GetAxisRaw("Horizontal") * acceleration * fixedDeltaTime, 0);
#elif UNITY_ANDROID || UNITY_IOS
            if (touchCount > 0)
            {
                force = new(acceleration * fixedDeltaTime, 0);
                if (GetTouch(0).position.x <= camera.pixelWidth / 2)
                    force = -force;
            }
            else
                force = Vector2.zero; 
#endif
            if (!dead)
            {
                switch (typeOfMoving)
                {
                    case TypeOfMoving.Force:
                        rb.AddForce(force * 100); break;
                    default:
                        rb.MovePosition(rb.position + force); break;
                }
            }
        }

        private void StopAll()
        {
            dead = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }
}