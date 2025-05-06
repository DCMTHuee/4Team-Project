using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace MoonYoHanStudy
{
    public abstract class StateBase<T> where T : ObjectStatusBase
    {
        protected T Object;

        protected Animator animator;

        public StateBase(T Object)
        {
            this.Object = Object;
        }

        // �� ��ӹ��� �ڽ� ������Ʈ ������ �׼ǵ� >> ���ΰ��� �ڽĵ��� ���� ����.
        protected Action _onEnter;
        protected Action _onUpdate;
        protected Action _onExit;

        // ������ �߰��ϴ� �Լ� >> new����Ÿ��(this)._onEnter ��..�� �� �ֱ�
        // ��ȯ�ϴ� ���� (state._onEnter += listener, this)���� �ι�° ���ڰ�(Item2)�� ��ȯ
        public StateBase<T> AddOnEnter(StateBase<ObjectStatusBase> state, Action listener) => (state._onEnter += listener, this).Item2;

        public StateBase<T> AddOnUpdate(StateBase<ObjectStatusBase> state, Action listener) => (state._onUpdate += listener, this).Item2;

        public StateBase<T> AddOnExit(StateBase<ObjectStatusBase> state, Action listener) => (state._onExit += listener, this).Item2;

        // ������ �����ϴ� �Լ�
        public virtual void InvokeOnEnter() => _onEnter?.Invoke();
        public virtual void InvokeOnUpdate() => _onUpdate?.Invoke();
        public virtual void InvokeOnExit() => _onExit?.Invoke();

        // ������ �ʱ�ȭ�ϴ� �Լ�
        public void ClearOnEnter() => _onEnter = null;
        public void ClearOnUpdate() => _onUpdate = null;
        public void ClearOnExit() => _onExit = null;
    }

    public class IdleState : StateBase<ObjectStatusBase>
    {
        public IdleState(ObjectStatusBase Object) : base(Object) { }

        public override void InvokeOnEnter()
        {
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("idle ��");
            base.InvokeOnUpdate(); // �̺�Ʈ ������ �� ����
                                   // �߰� �ൿ
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // �̺�Ʈ ������ �� ����
                                 // �߰� �ൿ
        }
    }

    public class MoveState : StateBase<ObjectStatusBase>
    {
        public MoveState(ObjectStatusBase Object) : base(Object) { }

        public override void InvokeOnEnter()
        {
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("Move ��");
            base.InvokeOnUpdate(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }
    }

    public class AttackState : StateBase<ObjectStatusBase>
    {
        public AttackState(ObjectStatusBase Object) : base(Object) { }

        public override void InvokeOnEnter()
        {
            Object.TryGetComponent(out animator);
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("Attack ��");
            base.InvokeOnUpdate(); // �̺�Ʈ ������ �� ����
                                   // �߰� �ൿ
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // �̺�Ʈ ������ �� ����
                                 // �߰� �ൿ
        }
    }

    public class HitState : StateBase<ObjectStatusBase>
    {
        public HitState(ObjectStatusBase Object) : base(Object) { }

        public override void InvokeOnEnter()
        {
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("Hit ��");
            base.InvokeOnUpdate(); // �̺�Ʈ ������ �� ����
                                   // �߰� �ൿ
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // �̺�Ʈ ������ �� ����
                                 // �߰� �ൿ
        }
    }
}


/*
 protected T Object;

        private event Action _onEnter;
        private event Action _onUpdate;
        private event Action _onExit;

        public void AddOnEnter(Action listener) => _onEnter += listener;
        public void AddOnUpdate(Action listener) => _onUpdate += listener;
        public void AddOnExit(Action listener) => _onExit += listener;

        public StateBase(T Object)
        {
            this.Object = Object;
        }

        public virtual void Enter()
        {
            _onEnter?.Invoke();
        }

        public virtual void Update()
        {
            _onUpdate?.Invoke();
        }

        public virtual void Exit()
        {
            _onExit?.Invoke();
        }
    }

    public class MoveState : StateBase<ObjectStatusBase>
    {
        public MoveState(ObjectStatusBase Object) : base(Object) { }

        public override void Enter()
        {
            Debug.Log("Move ���� ����");
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            Debug.Log("Move ���� ����");
            base.Exit();
        }
    }

    public class AttackState : StateBase<ObjectStatusBase>
    {

        public AttackState(ObjectStatusBase Object) : base(Object) { }

        public override void Enter()
        {
            Debug.Log("Attack ���� ����");
            base.Enter();

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            Debug.Log("Attack ���� ����");
            base.Exit();
        }
    }

    public class HitState : StateBase<ObjectStatusBase>
    {


        public HitState(ObjectStatusBase Object) : base(Object) { }

        public override void Enter()
        {
            Debug.Log("Hit ���� ����");
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            Debug.Log("Hit ���� ����");
            base.Exit();
        }
    }
 
 */