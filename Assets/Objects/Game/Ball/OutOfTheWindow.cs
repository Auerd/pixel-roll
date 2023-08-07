using UnityEngine;
using UnityEngine.Events;

namespace Game.Ball
{
    public class OutOfTheWindow : MonoBehaviour
    {
        [SerializeField] UnityEvent exitWindow;
        [SerializeField] Canvas canvas;

        private Rect canvasRect;
        private new CircleCollider2D collider;
        private Vector2 mainCanvasSize;
        private bool lastState;

        private void Start()
        {
            canvasRect = canvas.GetComponent<RectTransform>().rect;
            collider = GetComponent<CircleCollider2D>();
            mainCanvasSize = 2 * collider.radius * transform.localScale;
        }

        private void Update()
        {
            if (lastState != IsInTheWindow)
            {
                if (!IsInTheWindow && exitWindow != null)
                    exitWindow.Invoke();
                lastState = IsInTheWindow;
            }
        }

        private bool IsInTheWindow
        {
            get
            {
                float x, y;
                x = transform.localPosition.x;
                y = transform.localPosition.y;

                return x > -mainCanvasSize.x
                    && x < canvasRect.width + mainCanvasSize.x
                    && y > -mainCanvasSize.y
                    && y < canvasRect.height + mainCanvasSize.y;
            }
        }
    }
}