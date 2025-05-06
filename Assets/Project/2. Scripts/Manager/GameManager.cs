using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class GameManager : SingletoneBase<GameManager, DontDestroyOnLoadOff>
    {

        [HideInInspector] public PlayerController Player;
        [HideInInspector] public Lightbringer_Stone Lightbringer_Stone;

        public List<EnemyController> Enemys;

        protected override void Awake()
        {
            Player = FindAnyObjectByType<PlayerController>();
            Lightbringer_Stone = FindAnyObjectByType<Lightbringer_Stone>();

/*            // 씬에서 활성화된 모든 EnemyController 찾기 (기본 옵션은 활성 오브젝트만 찾음)
            EnemyController[] foundEnemies = Object.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

            Enemys = new List<EnemyController>(foundEnemies);*/

        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public bool isForceCursorVisible = false;

        public void SetCursorVisible(bool isVisible)
        {
            Cursor.visible = isVisible || isForceCursorVisible;
            Cursor.lockState = isVisible || isForceCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
