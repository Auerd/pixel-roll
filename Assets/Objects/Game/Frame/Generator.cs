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
        /// mainPool.Return() instead of Destroy()
        /// </summary>
        protected ObjectPool mainPool;

        protected void Awake()
        {
            mainPool = new();
        }

        protected void Start()
        {
            canvasRect = canvas.GetComponent<RectTransform>().rect;
        }


        protected bool IsRectInCanvas(in Rect rect)
        {
            Vector2 rectPos = new(rect.x, rect.y);
            Vector2 rectWidth = new(rect.width, 0);
            Vector2 rectHeight = new(0, rect.height);
            return IsLeastOneDotInCanvas(
                rectPos,                        
                rectPos + rectWidth,            
                rectPos + rectHeight,           
                rectPos + rectWidth + rectHeight
            );
        }

        protected bool IsLeastOneDotInCanvas(params Vector2[] dots)
        {
            foreach (var dot in dots)
                if (IsDotInCanvas(dot))
                    return true;
            return false;
        }

        protected bool IsDotInCanvas(in Vector2 dot)
        {
            return dot.x >= 0
                && dot.y >= 0
                && dot.x <= canvasRect.width
                && dot.y <= canvasRect.height;
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