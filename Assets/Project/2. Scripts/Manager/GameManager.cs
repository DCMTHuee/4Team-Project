using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [HideInInspector] public PlayerController Player;
        [HideInInspector] public Lightbringer_Stone Lightbringer_Stone;

        public List<EnemyController> enemys = new List<EnemyController>();

        private void Awake()
        {
            Instance = this;

            Player = FindAnyObjectByType<PlayerController>();
            Lightbringer_Stone = FindAnyObjectByType<Lightbringer_Stone>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private bool isForceCursorVisible = false;

        public void SetCursorVisible(bool isVisible)
        {
            Cursor.visible = isVisible || isForceCursorVisible;
            Cursor.lockState = isVisible || isForceCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
