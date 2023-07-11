using Game.Frame.BonusSystem;
using UnityEngine;

namespace Game
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] float firstSpeed;
        [SerializeField, Range(0f, 5f)] float acceleration;
        [SerializeField, Min(1)] int targetFrameRate = 60;
        [SerializeField] Skin skin;
        [SerializeField] GameObject deathScreen;
        
        public static Vector3 Speed { get; private set; }

        #region Singleton

        private GameManager() { }
        public static GameManager Instance { get; private set; }

        #endregion

        private void Awake()
        {
            Instance = this;
            Application.targetFrameRate = targetFrameRate + 1;
            deathScreen.SetActive(false);
        }

        private void Start()
        {
            #region Input Handle

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            #endregion
            #region Moving Objects At The Scene

            Speed = new Vector3(0, Instance.firstSpeed);

            #endregion
        }

        private void Update()
        {
            #region Input Handle

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            #endregion
            #region Moving Objects At The Scene

            Speed += new Vector3(0, Instance.acceleration * 1E-4f);

            #endregion
        }

        public void YouLose() => deathScreen.SetActive(true);
    }
}