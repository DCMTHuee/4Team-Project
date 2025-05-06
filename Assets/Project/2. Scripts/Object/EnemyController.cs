using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace MoonYoHanStudy
{
    public class EnemyController : ObjectStatusBase
    {
        [SerializeField] private MonsterType monsterType;
        [SerializeField] private EnemyData enemyData; // ���� ������

        [SerializeField] RectTransform HP_bar;

        // private StatusEffect statusEffect = StatusEffect.None; // �����̻��� ����


        [SerializeField] float currentHP; // ���� ü��
        [SerializeField] float currentST; // ���� ���׹̳�

        float moveSpeed; // �̵� ���ǵ�

        // bool canAction = true;
        bool canMove = true;

        private Transform playerPosition;
        private Transform lightbringer_Stone;

        public Dictionary<string, float> cumulative_Damage = new Dictionary<string, float>(); // ���� ��������

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
            if (canMove) // ���� �� �� ��
            {
                base.Update();

                currentState?.InvokeOnUpdate();

                // TransitionToState(new IdleState(this));
            }
        }


    }
}
