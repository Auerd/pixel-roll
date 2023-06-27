using UnityEngine;
using static UnityEngine.Input;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Controller : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        private Rigidbody2D rb;
        private Vector2 force;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            force = new Vector2(GetAxisRaw("Horizontal") * speed, 0);
            rb.AddForce(force);
        }
    }
}