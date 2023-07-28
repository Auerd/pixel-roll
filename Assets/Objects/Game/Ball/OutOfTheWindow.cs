using UnityEngine;

namespace Game.Ball
{
    public class OutOfTheWindow : MonoBehaviour
    {
        [SerializeField] Event @event;
        [SerializeField] Canvas canvas;

        private Rect canvasRect;
        private new CircleCollider2D collider;
        private Vector2 mainCanvasSize;

        private void Start()
        {
            canvasRect = canvas.GetComponent<RectTransform>().rect;
            collider = GetComponent<CircleCollider2D>();
            mainCanvasSize = 2 * collider.radius * transform.localScale;
        }

        private void Update()
        {
            if (IsBallOutOfTheWindow)
                @event.Raise();
        }

        private bool IsBallOutOfTheWindow
        {
            get
            {
                float x, y;
                x = transform.localPosition.x;
                y = transform.localPosition.y;

                return x < -mainCanvasSize.x
                    || x > canvasRect.width + mainCanvasSize.x
                    || y < -mainCanvasSize.y
                    || y > canvasRect.height + mainCanvasSize.y;
            }
        }
    }
}