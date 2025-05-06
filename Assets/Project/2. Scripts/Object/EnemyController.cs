using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace MoonYoHanStudy
{
    public class EnemyController : ObjectStatusBase
    {
        [SerializeField] private MonsterType monsterType;
        [SerializeField] private EnemyData enemyData; // 몬스터 데이터

        [SerializeField] RectTransform HP_bar;

        // private StatusEffect statusEffect = StatusEffect.None; // 상태이상의 상태


        [SerializeField] float currentHP; // 현재 체력
        [SerializeField] float currentST; // 현재 스테미너

        float moveSpeed; // 이동 스피드

        // bool canAction = true;
        bool canMove = true;

        private Transform playerPosition;
        private Transform lightbringer_Stone;

        public Dictionary<string, float> cumulative_Damage = new Dictionary<string, float>(); // 누적 데미지량

        [Header("Target")]
        public TargetObject TARGET_OBJECT;
        public Vector3 targetPos;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Init();
        }

        void Init()
        {
            playerPosition = GameManager.Singletone.Player.transform;
            lightbringer_Stone = GameManager.Singletone.Lightbringer_Stone.transform;

            var data = enemyData.GetData(monsterType);

            AttackPoint = data.AttackPoint;

            MaxHP = data.MaxHP;
            currentHP = MaxHP;

            MaxST = data.MaxST;
            currentST = MaxST;

            moveSpeed = data.Speed;
        }

        public override void TakeDamage(float amount)
        {
            currentHP -= amount;
            currentHP = Mathf.Clamp(currentHP, 0, MaxHP);
            HP();
        }

        public void HP()
        {
            HP_bar.localScale = Vector3.right * (currentHP / MaxHP) + Vector3.up;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (canMove) // 조건 손 볼 것
            {
                base.Update();

                currentState?.InvokeOnUpdate();

                // TransitionToState(new IdleState(this));
            }
        }


    }
}
