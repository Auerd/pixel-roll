using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 1f)]
        private float firstSpeed;
        [Range(0f, .5f)] 
        [SerializeField] 
        private float acceleration;
        [SerializeField]
        private Skin skin;

        public static Vector3 Speed { get; private set; }
        public Skin Skin { get { return skin; } }

        #region Singleton

        private GameManager() { }
        public static GameManager Instance { get; private set; }

        #endregion

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            #region Input Handle

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            #endregion
            #region Moving Objects At Scene

            Speed = new Vector3(0, Instance.firstSpeed);

            #endregion
        }

        private void Update()
        {
            #region Input Handle

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            #endregion
            #region Moving Objects At Scene

            Speed += new Vector3(0, Instance.acceleration * .01f);

            #endregion
        }
    }
}