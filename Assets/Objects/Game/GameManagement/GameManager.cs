﻿using UnityEditor;
using UnityEngine;

namespace Game.GameManagement
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] float speed = 1;
        [SerializeField, Range(0f, 5f)] float acceleration = 1;
        [SerializeField, Min(1)] uint targetFrameRate, healthPoints;
        [SerializeField] GameObject deathscreen, healthbar, scorebar = null;
        [SerializeField] Event spikeCollision, bonusCollision, death = null;
        [SerializeField] Skin skin = null;
        
        #region Singleton

        private GameManager() { }
        private static GameManager instance;

        #endregion
        
        public static uint HealthPoints { get { return instance.healthPoints; } }
        public static Skin Skin { get { return instance.skin; } }
          public static Vector3 Speed 
        { 
            get { return new(0, instance.speed); } 
        }


        private void Awake()
        {
            instance = this;
            Application.targetFrameRate = (int)targetFrameRate + 1;
            deathscreen.SetActive(false);
            spikeCollision.Subscribe(OnSpikeCollide);
        }

        private void Update()
        {
            speed += acceleration * 1E-4f;
        }

        public void OnSpikeCollide()
        {
            healthPoints--;
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}