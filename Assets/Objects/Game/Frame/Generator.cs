using UnityEngine;

namespace Game.Frame
{
    public abstract class Generator : MonoBehaviour
    {
        [SerializeField] protected Canvas canvas;
        protected Rect canvasRect;
        /// <summary>
        /// Main pool of objects that you generate.
        /// You should use mainPool.Get() instead of Instantiate() and 
        /// mainPool.Return() instead of Destroy(). 
        /// This will reduce the load on the processor
        /// </summary>
        protected ObjectPool mainPool;

        /// <summary>
        /// Use base.Awake() in Awake function
        /// </summary>
        protected void Awake()
        {
            mainPool = new();
        }

        /// <summary>
        /// Use base.Start() in Start() function
        /// </summary>
        protected void Start()
        {
            canvasRect = canvas.GetComponent<RectTransform>().rect;
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        protected Vector3 GetCanvasSizeOf(BoxCollider2D collider)
        {
            return collider.size * collider.transform.localScale;
        }

        protected Vector3 GetCanvasSizeOf(CircleCollider2D collider)
        {
            return 2 * collider.radius * collider.transform.localScale;
        }
    }
}