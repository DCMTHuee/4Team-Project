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
        move,
        attack,
    }

    public enum TargetObject
    {
        Lightbringer_Stone,
        Player,
        Trap_Object,
        Normal_Object,
    }

    public class Enemy_Test1 : Enemy_Base
    {
        public MonsterState MONSTER_STATE; // 몬스터의 행동 상태
        public MonsterType MONSTER_TYPE; // 몬스터의 종족(타입)
        public TargetObject TARGET_OBJECT;

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
            StartCoroutine(TargetUpdate());
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

                    TARGET_OBJECT = TargetObject.Lightbringer_Stone;
                    m_TargetName = lightbringer_Stone.gameObject.name;
                    m_TargetPos = lightbringer_Stone.position;

                    break;

                case MonsterType.Tree:

                    MaxHP = 2000;
                    curHP = MaxHP;

                    MaxST = 300;
                    curST = MaxST;

                    TARGET_OBJECT = TargetObject.Lightbringer_Stone;
                    m_TargetName = lightbringer_Stone.gameObject.name;
                    m_TargetPos = lightbringer_Stone.position;

                    break;

                case MonsterType.Boss:

                    MaxHP = 10000;
                    curHP = MaxHP;

                    MaxST = 10000;
                    curST = MaxST;

                    TARGET_OBJECT = TargetObject.Player;
                    m_TargetName = playerPosition.gameObject.name;
                    m_TargetPos = playerPosition.position;

                    break;
            }
        }// void MonsterInit(MonsterType monsterType)

        // Update is called once per frame
        void Update()
        {
            if (m_TargetName == null)
            {
                MonsterUpdate();
            }
        }// void Update()

        void MonsterUpdate()
        {
            switch(MONSTER_STATE)
            {
                case MonsterState.idle:
                    if (Vector3.Distance(m_TargetPos, transform.position) > 30)
                    {
                        // 아이들 행동
                    }
                    else if (Vector3.Distance(m_TargetPos, transform.position) < 20)
                    {
                        MONSTER_STATE = MonsterState.move;
                    }
                    break;

                case MonsterState.move:

                    // 무브 행동
                    MonsterMove();

                    if (Vector3.Distance(m_TargetPos, transform.position) > 20)
                    {
                        MONSTER_STATE = MonsterState.idle;
                    }
                    else if (Vector3.Distance(m_TargetPos, transform.position) < 1)
                    {
                        MONSTER_STATE = MonsterState.attack;
                    }
                    break;

                case MonsterState.attack:
                    if (Vector3.Distance(m_TargetPos, transform.position) > 1)
                    {
                        MONSTER_STATE = MonsterState.move;
                    }
                    else if (true)
                    {
                        // 어택 행동
                    }
                    break;
            }
        }// void MonsterUpdate()

        
        void MonsterIdle()
        {

        }

        void MonsterMove()
        {
            transform.position -= MovePoint(TARGET_OBJECT) * Time.deltaTime;
        }

        Vector3 MovePoint(TargetObject TARGET_OBJECT)
        {
            Vector3 vector3 = Vector3.zero;

            switch (TARGET_OBJECT)
            {
                case TargetObject.Lightbringer_Stone:
                    vector3 = (transform.position - playerPosition.position).normalized;
                    break;

                case TargetObject.Player:
                    vector3 = (transform.position - playerPosition.position).normalized;
                    break;
            }

            return vector3;
        }

        public override void MonsterAttack()
        {

        }

        IEnumerator TargetUpdate()
        {
            yield return new WaitForSeconds(15);

        TU:

            if (Vector3.Distance(transform.position, playerPosition.position) < Vector3.Distance(transform.position, lightbringer_Stone.position))
            {
                TARGET_OBJECT = TargetObject.Player;
            }
            else
            {
                TARGET_OBJECT = TargetObject.Lightbringer_Stone;
            }

            yield return new WaitForSeconds(5);

            goto TU;
        }// IEnumerator TargetUpdate()

        public override void EnemyHit()
        {
            throw new System.NotImplementedException();
        }
    }
}
