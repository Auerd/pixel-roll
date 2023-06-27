using UnityEngine;

namespace GameWindow
{
    [RequireComponent (typeof (Camera))]
    public class Window : MonoBehaviour
    {
        public Camera Display { get; private set; }

        private void Start ()
        {
            Display = GetComponent<Camera> ();
        }

        public bool WindowedDotInWindow(Vector2 dot)
        {
            Vector2 bottomLeft = Display.ScreenToWorldPoint(new Vector2());
            Vector2 topRight = Display.ScreenToWorldPoint(new Vector2(Display.pixelWidth, Display.pixelHeight));
            return 
                dot.x > bottomLeft.x &&
                dot.y > bottomLeft.y &&
                dot.x < topRight.x &&
                dot.y < topRight.y;
        }

        public bool DotInWindow(Vector2 objectPosition)
        {
            Vector2 windowedPosition = Display.ScreenToWorldPoint(objectPosition);
            return WindowedDotInWindow(windowedPosition);
        }
    }
}