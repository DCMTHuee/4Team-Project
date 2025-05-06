using System;
using System.Threading;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace MoonYoHanStudy
{
    public class StateBase<T> where T : ObjectStatusBase
    {
        public T Object;

        protected Animator animator;

        public StateBase(T Object)
        {
            this.Object = Object;
        }

        // �� ��ӹ��� �ڽ� ������Ʈ ������ �׼ǵ� >> ���ΰ��� �ڽĵ��� ���� ����.
        protected Action _onEnter;
        public Action _onUpdate;
        protected Action _onExit;

        // ������ �߰��ϴ� �Լ� >> new����Ÿ��(this)._onEnter ��..�� �� �ֱ�
        // ��ȯ�ϴ� ���� (state._onEnter += listener, this)���� �ι�° ���ڰ�(Item2)�� ��ȯ
        public StateBase<T> AddOnEnter(StateBase<ObjectStatusBase> state, Action listener)
        {
            state._onEnter += listener;
            return this;
        }


        public StateBase<T> AddOnUpdate(StateBase<ObjectStatusBase> state, Action listener)
        {
            state._onUpdate += listener;
            return this;
        }


        public StateBase<T> AddOnExit(StateBase<ObjectStatusBase> state, Action listener)
        {
            state._onExit += listener;
            return this;
        }

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
            Debug.Log("IdleState");
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {

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
            Debug.Log("MoveState");
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {

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
            Debug.Log("AttackState");
            Object.TryGetComponent(out animator);
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {

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
        public HitState(ObjectStatusBase Object) : base(Object) {  }

        public override void InvokeOnEnter()
        {
            Debug.Log("Hit ��");
            base.InvokeOnEnter(); // �̺�Ʈ ������ �� ����
                                  // �߰� �ൿ
        }

        public override void InvokeOnUpdate()
        {
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