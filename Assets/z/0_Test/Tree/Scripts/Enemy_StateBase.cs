using System.Threading;
using UnityEngine;

namespace MoonYoHanStudy
{
    public abstract class Enemy_StateBase
    {
        protected Enemy_Test1 monster;
        protected Vector3 TargetPos;

        public Enemy_StateBase(Enemy_Test1 monster)
        {
            this.monster = monster;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }

    public class IdleState : Enemy_StateBase
    {
        public IdleState(Enemy_Test1 monster) : base(monster) { }

        public override void Enter()
        {
            // �ִϸ��̼� ��ȯ ��
            Debug.Log("Idle ���� ����");
        }

        public override void Update()
        {
            float distance = Vector3.Distance(monster.transform.position, monster.m_TargetPos);
            if (distance < 20f)
            {
                monster.TransitionToState(new MoveState(monster));
            }
        }

        public override void Exit()
        {
            // ���� ���� �� �ʿ��� ó��
        }
    }

    public class MoveState : Enemy_StateBase
    {
        public MoveState(Enemy_Test1 monster) : base(monster) { }

        public override void Enter()
        {
            Debug.Log("Move ���� ����");
        }

        public override void Update()
        {
            monster.transform.position -= monster.MovePoint(monster.TARGET_OBJECT) * Time.deltaTime;

            float distance = Vector3.Distance(monster.transform.position, monster.m_TargetPos);
            if (distance < 1f)
            {
                monster.TransitionToState(new AttackState(monster));
            }
            else if (distance > 20f)
            {
                monster.TransitionToState(new IdleState(monster));
            }
        }

        public override void Exit()
        {
            Debug.Log("Move ���� ����");
        }
    }

    public class AttackState : Enemy_StateBase
    {
        public AttackState(Enemy_Test1 monster) : base(monster) { }

        public override void Enter()
        {
            Debug.Log("Attack ���� ����");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("Attack ���� ����");
        }
    }

    public class HitState : Enemy_StateBase
    {
        public HitState(Enemy_Test1 monster) : base(monster) { }

        public override void Enter()
        {
            Debug.Log("Hit ���� ����");
        }

        public override void Update()
        {

        }

        public override void Exit()
        {
            Debug.Log("Hit ���� ����");
        }
    }
}
