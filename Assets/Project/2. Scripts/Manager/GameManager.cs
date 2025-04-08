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

        public List<Enemy_Base> enemys = new List<Enemy_Base>();

        private void Awake()
        {
            Instance = this;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Player = FindAnyObjectByType<PlayerController>();
            Lightbringer_Stone = FindAnyObjectByType<Lightbringer_Stone>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
