﻿using Game.Frame.BonusSystem;
using UnityEngine;

namespace Game
{
    public sealed class GameManager : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] float firstSpeed;
        [Range(0f, 5f)] [SerializeField] float acceleration;
        [Min(1)] [SerializeField] int targetFrameRate = 60;
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

            Speed += new Vector3(0, Instance.acceleration * .0001f);

            #endregion
        }

        public void YouLose() => deathScreen.SetActive(true);
    }
}