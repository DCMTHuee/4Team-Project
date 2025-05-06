using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEngine.Rendering.DebugUI;

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

        [SerializeField] Transform attackPosition; // 공격지점

        private Transform playerPosition;
        private Transform lightbringer_Stone;

        public Dictionary<string, float> cumulative_Damage = new Dictionary<string, float>(); // 누적 데미지량

        [Header("Target")]
        public TargetObject TARGET_OBJECT;
        public Vector3 targetPos;

        public Effect Normal_Attack_Effect;

        [SerializeField] private SkillData skill;

        private void Awake()
        {

        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Init();

            StateInit();
        }

        void StateInit()
        {
            CurrentState = new(this);
            IdleState = new(CurrentState.Object);
            MoveState = new(CurrentState.Object);
            AttackState = new(CurrentState.Object);
            HitState = new(CurrentState.Object);

            CurrentState.AddOnUpdate(IdleState, TargetDistance).AddOnUpdate(MoveState, TargetDistance).AddOnUpdate(AttackState, TargetDistance).AddOnUpdate(MoveState, TargetDistance); ;

            CurrentState.AddOnUpdate(MoveState, Move);

            CurrentState.AddOnUpdate(AttackState, Attack);

            TransitionToState(IdleState);
        }

        void Init()
        {
            playerPosition = GameManager.Singletone.Player.transform;
            lightbringer_Stone = GameManager.Singletone.Lightbringer_Stone.transform;

            var enemydata = enemyData.GetData(monsterType);

            AttackPoint = enemydata.AttackPoint;

            MaxHP = enemydata.MaxHP;
            currentHP = MaxHP;

            MaxST = enemydata.MaxST;
            currentST = MaxST;

            MoveSpeed = enemydata.Speed;

            CharacterController = GetComponent<CharacterController>();

            Normal_Attack_Effect.AttackDamageValue = AttackPoint;
            Normal_Attack_Effect.PersentValue = skill.GetData("Normal_Attack").AttackPercent[0];
        }

        float timer;

        IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(3);

            CanAttack = true;

            yield return null;
        }

        void Attack()
        {
            if (CanAttack)
            {
                GameObject obj = Instantiate(skill.GetData("Normal_Attack").skillPrefab);
                obj.transform.position = attackPosition.position;
                CanAttack = false;

                StartCoroutine(AttackDelay());
            }
        }

        // 나중에 회전은 네브메쉬를 통해 지나가는 경로를 바라보게 할 예정
        // private float rotationSpeed = 10;          // 회전값 세로축 (좌 우)

        void Move()
        {
            if (CanAttack)
            {
                Vector3 adjustMovement = (playerPosition.position - this.gameObject.transform.position).normalized * MoveSpeed * Time.deltaTime; ;
                CharacterController.Move(adjustMovement);

                transform.LookAt(new Vector3(playerPosition.position.x, transform.position.y, playerPosition.position.z));

            }
        }

        public float ClampAngle(float angle, float min, float max)
        {
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f; // -180 ~ 180도로 정규화
            return Mathf.Clamp(angle, min, max);
        }

        void TargetDistance()
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, playerPosition.position);

            if (10 <= distance && distance < 30)
            {
                TransitionToState(IdleState);
            }
            else if (2 <= distance && distance < 10)
            {
                TransitionToState(MoveState);
            }
            else if (0 <= distance && distance < 2)
            {
                TransitionToState(AttackState);
            }
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
            if (CanMove) // 조건 손 볼 것
            {
                base.Update();

                CurrentState?.InvokeOnUpdate();
            }
        }


    }
}
