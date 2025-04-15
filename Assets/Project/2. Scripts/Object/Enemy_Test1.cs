using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class Enemy_Test1 : ObjectStatusBase
    {
        [SerializeField] private EnemyData enemyData;

        public MonsterState MONSTER_STATE; // 몬스터의 행동 상태
        public MonsterType MONSTER_TYPE; // 몬스터의 종족(타입)
        public TargetObject TARGET_OBJECT;

        public StatusEffect statusEffect = StatusEffect.None;

        [SerializeField] float curHP;
        [SerializeField] float curST;

        private Transform playerPosition;
        private Transform lightbringer_Stone;

        public Dictionary<string, float> cumulative_Damage = new Dictionary<string, float>(); // 누적 데미지량

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
            var data = enemyData.GetData(monsterType);

            MaxHP = data.MaxHP;
            curHP = MaxHP;
            MaxST = data.MaxST;
            curST = MaxST;

            switch (monsterType)
            {
                case MonsterType.None:
                    TARGET_OBJECT = TargetObject.Lightbringer_Stone;
                    m_TargetPos = lightbringer_Stone.position;

                    break;

                case MonsterType.Tree:
                    TARGET_OBJECT = TargetObject.Lightbringer_Stone;
                    m_TargetPos = lightbringer_Stone.position;

                    break;

                case MonsterType.Boss:
                    TARGET_OBJECT = TargetObject.Player;
                    m_TargetPos = playerPosition.position;

                    break;
            }

            TransitionToState(new IdleState(this));

        }// void MonsterInit(MonsterType monsterType)

        // Update is called once per frame
        protected override void Update()
        {
            if (m_TargetName != null)
            {
                currentState?.Update();
            }
        }// void Update()

        void CCUpdate()
        {
            if ((statusEffect & StatusEffect.Stunned) != 0)
            {
                // 기절 상태 - 아무 행동 불가
                return;
            }

            if ((statusEffect & StatusEffect.Slowed) != 0)
            {
                // 기절 상태 - 아무 행동 불가
                return;
            }
        }

        public void MonsterMove()
        {
            transform.position -= MovePoint(TARGET_OBJECT) * Time.deltaTime;
        }

        public Vector3 MovePoint(TargetObject targetObject)
        {
            Vector3 vector3 = Vector3.zero;

            switch(targetObject)
            {
                case TargetObject.Lightbringer_Stone:
                    m_TargetPos = lightbringer_Stone.position;
                    break;

                case TargetObject.Player:
                    m_TargetPos = playerPosition.position;
                    break;
            }

            vector3 = (transform.position - m_TargetPos).normalized;

            return vector3;
        }

        private Enemy_StateBase currentState;

        public void TransitionToState(Enemy_StateBase newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        IEnumerator TargetUpdate()
        {
            yield return new WaitForSeconds(15);

        TU:

            Debug.Log("TargetUpdate");

            if (Vector3.Distance(transform.position, playerPosition.position) < Vector3.Distance(transform.position, lightbringer_Stone.position))
            {
                Debug.Log("Player");
                TARGET_OBJECT = TargetObject.Player;

            }
            else
            {
                Debug.Log("Lightbringer_Stone");
                TARGET_OBJECT = TargetObject.Lightbringer_Stone;
            }

            yield return new WaitForSeconds(5);

            goto TU;
        }// IEnumerator TargetUpdate()

        public override void TakeDamage(float Damage)
        {
            curHP -= Damage;
        }
    }
}
