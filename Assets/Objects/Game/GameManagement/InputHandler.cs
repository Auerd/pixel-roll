using UnityEngine;

namespace Game.GameManagement
{
    public class InputHandler : MonoBehaviour
    {
        void Start()
        {
#if UNITY_STANDALONE
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
#endif
        }

        void Update()
        {
#if UNITY_STANDALONE
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
#endif
        }
    }
}