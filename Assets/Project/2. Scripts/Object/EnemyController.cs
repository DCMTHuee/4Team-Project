using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class EnemyController : ObjectStatusBase
    {
        [SerializeField] private MonsterType monsterType;
        [SerializeField] private EnemyData enemyData; // ���� ������

        private Enemy_StateBase currentState; // ���� �ൿ ����
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
            playerPosition = GameManager.Instance.Player.transform;
            lightbringer_Stone = GameManager.Instance.Lightbringer_Stone.transform;

            var data = enemyData.GetData(monsterType);

            MaxHP = data.MaxHP;
            currentHP = MaxHP;

            MaxST = data.MaxST;
            currentST = MaxST;

            moveSpeed = data.Speed;
        }

        public void TransitionToState(Enemy_StateBase newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public override void TakeDamage(float amount)
        {
            throw new System.NotImplementedException();
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (canMove) // ���� �� �� ��
            {
                base.Update();
            }
        }


    }
}
