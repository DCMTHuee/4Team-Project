using UnityEngine;

namespace MoonYoHanStudy
{
    public class EnemyController : ObjectStatusBase
    {
        [SerializeField] private MonsterType monsterType;
        [SerializeField] private EnemyData enemyData; // 몬스터 데이터

        private Enemy_StateBase currentState; // 현재 상태

        float currentHP; // 현재 체력
        float currentST; // 현재 스테미너

        float moveSpeed; // 이동 스피드

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
