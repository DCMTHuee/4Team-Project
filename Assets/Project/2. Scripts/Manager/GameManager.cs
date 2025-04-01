using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [HideInInspector] public Player_Move player;
        public List<Enemy_Base> enemys = new List<Enemy_Base>();

        private void Awake()
        {
            instance = this;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            player = FindAnyObjectByType<Player_Move>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
