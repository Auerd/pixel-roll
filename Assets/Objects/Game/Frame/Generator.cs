using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class Generator : MonoBehaviour
    {
        [SerializeField] protected Canvas canvas;
        protected Rect canvasRectTransform;
        protected ObjectPool mainPool;

        protected void Awake()
        {
            mainPool = new();
        }

        protected void Start()
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>().rect;
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
                && dot.x <= canvasRectTransform.width
                && dot.y <= canvasRectTransform.height;
        }
    }
}