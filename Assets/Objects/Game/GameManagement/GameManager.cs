using UnityEngine;

namespace Game.GameManagement
{
    public sealed class GameManager : MonoBehaviour
    {
        #region Input

        [SerializeField, Range(0f, 1f)] float initialSpeed = 1;
        [SerializeField, Range(0f, 5f)] float acceleration = 1;
        [SerializeField, Min(1)] uint targetFrameRate, healthPoints;
        [SerializeField] Skin skin = null;
        [Space]
        [Header("UI")]
        [SerializeField] Canvas canvas; 
        [SerializeField] GameObject healthbar, scorebar, deathscreen;
        [Space]
        [Header("Events")]
        [SerializeField] Event spikeDeath;
        [SerializeField] Event fallDeath, bonusCollision;

        #endregion
        #region Singleton

        private GameManager() { }
        private static GameManager instance;

        #endregion
        
        public static uint HealthPoints { get => instance.healthPoints; }
        public static Skin Skin { get => instance.skin; }
        public static Vector3 Speed { get => new(0, instance.speed); }
        public static Canvas Canvas { get => instance.canvas; }

        private float speed;

        private void Awake()
        {
            instance = this;
            speed = initialSpeed;
            Application.targetFrameRate = (int)targetFrameRate + 1;
            deathscreen.SetActive(false);
            spikeDeath.Subscribe(deathFromSpikes);
        }

        private void Update()
        {
            speed += acceleration * 1E-4f;
        }

        public void deathFromSpikes()
        {
            healthPoints--;
#if UNITY_EDITOR
            //EditorApplication.isPlaying = false;
#endif
        }
    }
}