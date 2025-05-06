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

        // 각 상속받을 자식 오브젝트 각각의 액션들 >> 서로간의 자식들은 따로 본다.
        protected Action _onEnter;
        protected Action _onUpdate;
        protected Action _onExit;

        // 구독을 추가하는 함수 >> new상태타입(this)._onEnter 등..에 값 넣기
        // 반환하는 값은 (state._onEnter += listener, this)에서 두번째 인자값(Item2)을 반환
        public StateBase<T> AddOnEnter(StateBase<ObjectStatusBase> state, Action listener) => (state._onEnter += listener, this).Item2;

        public StateBase<T> AddOnUpdate(StateBase<ObjectStatusBase> state, Action listener) => (state._onUpdate += listener, this).Item2;

        public StateBase<T> AddOnExit(StateBase<ObjectStatusBase> state, Action listener) => (state._onExit += listener, this).Item2;

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
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("idle 중");
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
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("Move 중");
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
            Object.TryGetComponent(out animator);
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("Attack 중");
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
        public HitState(ObjectStatusBase Object) : base(Object) { }

        public override void InvokeOnEnter()
        {
            base.InvokeOnEnter(); // 이벤트 구독된 것 실행
                                  // 추가 행동
        }

        public override void InvokeOnUpdate()
        {
            Debug.Log("Hit 중");
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
            Debug.Log("Move 상태 진입");
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            Debug.Log("Move 상태 종료");
            base.Exit();
        }
    }

    public class AttackState : StateBase<ObjectStatusBase>
    {

        public AttackState(ObjectStatusBase Object) : base(Object) { }

        public override void Enter()
        {
            Debug.Log("Attack 상태 진입");
            base.Enter();

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            Debug.Log("Attack 상태 종료");
            base.Exit();
        }
    }

    public class HitState : StateBase<ObjectStatusBase>
    {


        public HitState(ObjectStatusBase Object) : base(Object) { }

        public override void Enter()
        {
            Debug.Log("Hit 상태 진입");
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            Debug.Log("Hit 상태 종료");
            base.Exit();
        }
    }
 
 */