using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public enum MonsterType
    {
        None,
        Tree,
        Boss,
    }

    public enum MonsterState
    {
        idle,
    }

    public class Enemy_Test1 : Enemy_Base
    {
        public MonsterState MONSTER_STATE; // 몬스터의 행동 상태
        public MonsterType MONSTER_TYPE; // 몬스터의 종족(타입)

        [SerializeField] float curHP;
        [SerializeField] float curST;

        private Transform playerPosition;
        private Transform lightbringer_Stone;

        public Dictionary<string, float> cumulative_Damage = new Dictionary<string, float>();

        [Header("Target")]
        public string m_TargetName;
        public Vector3 m_TargetPos;

        [Header("Attack")]
        public GameObject Skill;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerPosition = GameManager.Instance.Player.transform;
            lightbringer_Stone = GameManager.Instance.Lightbringer_Stone.transform;

            MonsterInit(MONSTER_TYPE);
        } // void Start()

        void MonsterInit(MonsterType monsterType)
        {
            switch(monsterType)
            {
                case MonsterType.None:

                    MaxHP = 1000;
                    curHP = MaxHP;

                    MaxST = 100;
                    curST = MaxST;

                    m_TargetName = lightbringer_Stone.gameObject.name;
                    m_TargetPos = lightbringer_Stone.position;

                    break;

                case MonsterType.Tree:

                    MaxHP = 2000;
                    curHP = MaxHP;

                    MaxST = 300;
                    curST = MaxST;

                    m_TargetName = lightbringer_Stone.gameObject.name;
                    m_TargetPos = lightbringer_Stone.position;

                    break;

                case MonsterType.Boss:

                    MaxHP = 10000;
                    curHP = MaxHP;

                    MaxST = 10000;
                    curST = MaxST;

                    m_TargetName = playerPosition.gameObject.name;
                    m_TargetPos = playerPosition.position;

                    break;
            }
        }// void MonsterInit(MonsterType monsterType)

        // Update is called once per frame
        void Update()
        {
            MonsterUpdate();
        }// void Update()

        void MonsterUpdate()
        {
            m_TargetName = playerPosition.gameObject.name;
            m_TargetPos = playerPosition.position;
        }// void MonsterUpdate()

        
        IEnumerator TargetUpdate()
        {
            bool startTargetCheck = false;
            float waitForSeconds = 15;
            float IwaitForSeconds = 5;

            if (startTargetCheck)
            {
                startTargetCheck = true;
                yield return new WaitForSeconds(waitForSeconds);
            }

        I:
            if (Vector3.Distance(transform.position, playerPosition.position) < Vector3.Distance(transform.position, lightbringer_Stone.position))
            {
                m_TargetName = playerPosition.gameObject.name;
                m_TargetPos = playerPosition.position;
            }
            else
            {
                m_TargetName = lightbringer_Stone.gameObject.name;
                m_TargetPos = lightbringer_Stone.position;
            }

            yield return new WaitForSeconds(IwaitForSeconds);

            goto I;
        }// IEnumerator TargetUpdate()
    }
}
