using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Input;
using static UnityEngine.Time;

namespace Game.Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Controller : MonoBehaviour
    {
        private enum TypeOfMoving
        {
            Force,
            Impulse,
            Retro,
        }
        [SerializeField] private TypeOfMoving typeOfMoving;
        [SerializeField] private float acceleration = 0.5f;
        [SerializeField] private UnityEvent OnSpikeCollision;
        private Rigidbody2D rb;

        #region Singleton

        private Controller () { }
        public static Controller Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        #endregion


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            Vector2 force = new(GetAxisRaw("Horizontal") * acceleration * fixedDeltaTime, 0);
            switch (typeOfMoving)
            {
                case TypeOfMoving.Force:
                    rb.AddForce(force * 100); break;
                case TypeOfMoving.Impulse:
                    rb.AddForce(force * 10, ForceMode2D.Impulse); break;
                default:
                    rb.MovePosition(rb.position + force * .4f); break; 
            }
        }
    }
}