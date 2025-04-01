using System.Collections;
using UnityEngine;

namespace MoonYoHanStudy
{
    public enum MonsterType
    {
        None,
        Tree,
    }

    public enum MonsterState
    {
        idle,
    }

    public class Enemy_Test1 : Enemy_Base
    {
        public MonsterState MONSTER_STATE;
        public MonsterType MONSTER_TYPE;

        [SerializeField] float curHP;
        [SerializeField] float curST;

        private Vector3 playerPosition;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            MonsterInit(MONSTER_TYPE);
            playerPosition = GameManager.instance.player.transform.position;
            
        }

        // Update is called once per frame
        void Update()
        {
            MonsterUpdate();
        }

        void MonsterUpdate()
        {

        }

        void MonsterInit(MonsterType monsterType)
        {
            switch(monsterType)
            {
                case MonsterType.None:

                    MaxHP = 1000;
                    curHP = MaxHP;

                    MaxST = 100;
                    curST = MaxST;

                    break;

                case MonsterType.Tree:

                    MaxHP = 2000;
                    curHP = MaxHP;

                    MaxST = 300;
                    curST = MaxST;

                    break;
            }
        }
    }
}
