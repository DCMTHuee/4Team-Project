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

        // 각 상속받을 자식 오브젝트 각각의 액션들 >> 서로간의 자식들은 따로 본다.
        protected Action _onEnter;
        public Action _onUpdate;
        protected Action _onExit;

        // 구독을 추가하는 함수 >> new상태타입(this)._onEnter 등..에 값 넣기
        // 반환하는 값은 (state._onEnter += listener, this)에서 두번째 인자값(Item2)을 반환
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

        // 구독을 실행하는 함수
        public virtual void InvokeOnEnter() => _onEnter?.Invoke();
        public virtual void InvokeOnUpdate() => _onUpdate?.Invoke();
        public virtual void InvokeOnExit() => _onExit?.Invoke();

        // 구독을 초기화하는 함수
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
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {

            base.InvokeOnUpdate(); // 이벤트 구독된 것 실행
                                   // 추가 행동
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // 이벤트 구독된 것 실행
                                 // 추가 행동
        }
    }

    public class MoveState : StateBase<ObjectStatusBase>
    {
        public MoveState(ObjectStatusBase Object) : base(Object) { }

        public override void InvokeOnEnter()
        {
            Debug.Log("MoveState");
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {

            base.InvokeOnUpdate(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }
    }

    public class AttackState : StateBase<ObjectStatusBase>
    {
        public AttackState(ObjectStatusBase Object) : base(Object) { }

        public override void InvokeOnEnter()
        {
            Debug.Log("AttackState");
            Object.TryGetComponent(out animator);
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {

            base.InvokeOnUpdate(); // 이벤트 구독된 것 실행
                                   // 추가 행동
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // 이벤트 구독된 것 실행
                                 // 추가 행동
        }
    }

    public class HitState : StateBase<ObjectStatusBase>
    {
        public HitState(ObjectStatusBase Object) : base(Object) {  }

        public override void InvokeOnEnter()
        {
            Debug.Log("Hit 중");
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {
            base.InvokeOnUpdate(); // 이벤트 구독된 것 실행
                                   // 추가 행동
        }

        public override void InvokeOnExit()
        {
            base.InvokeOnExit(); // 이벤트 구독된 것 실행
                                 // 추가 행동
        }
    }
}