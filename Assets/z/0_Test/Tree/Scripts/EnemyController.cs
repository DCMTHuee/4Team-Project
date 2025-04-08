using UnityEngine;

namespace MoonYoHanStudy
{
    public class EnemyController : ObjectStatusBase
    {
        [SerializeField] private MonsterType monsterType;
        [SerializeField] private EnemyData enemyData; // ���� ������

        private Enemy_StateBase currentState; // ���� ����

        float currentHP; // ���� ü��
        float currentST; // ���� ���׹̳�

        float moveSpeed; // �̵� ���ǵ�

        // bool canAction = true;
        bool canMove = true;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Init();
        }

        void Init()
        {
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
            base.Update();
        }


    }
}
