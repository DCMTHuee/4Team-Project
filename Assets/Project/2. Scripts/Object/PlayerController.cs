using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace MoonYoHanStudy
{
    public class PlayerController : ObjectStatusBase
    {
        [SerializeField] private PlayerType playerType;
        [SerializeField] private PlayerData playerData; // 플레이어 데이터
        [SerializeField] private GameObject CameraPivot; // 메인 카메라

        [SerializeField] private Skill skill;

        [SerializeField] float currentHP; // 현재 체력
        public RectTransform HP_bar;
        [SerializeField] float currentST; // 현재 스테미너

        [SerializeField] float rotationSpeed = 10; // 카메라 회전력

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Init();
        }

        void Init()
        {
            var data = playerData.GetData(playerType);

            AttackPoint = data.AttackPoint;

            MaxHP = data.MaxHP;
            currentHP = MaxHP;

            MaxST = data.MaxST;
            currentST = MaxST;

            MoveSpeed = data.Speed;

            ANIMATOR = GetComponent<Animator>();

            CharacterController = GetComponent<CharacterController>();

            InputManager.Singletone.MouseLeftPressed += Attack;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            PositionMove();

            Rotate();

            CurrentState?.InvokeOnUpdate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Monster"))
            {
                Debug.Log("몬스터가 닿음");
            }
        }

        #region Get/Set과 Switch/Change

        public override void TakeDamage(float amount)
        {
            currentHP -= amount;
        }

        public float GetCurrnetHP()
        {
            return currentHP;
        }

        public float GetCurrnetST()
        {
            return currentST;
        }

        public void SetActionSwitch(bool canMove, bool canRotate, bool canAttack)
        {
            CanMove = canMove;
            CanRotate = canRotate;
            CanAttack = canAttack;
        }

        public void SetCanMoveChange()
        {
            CanMove = !CanMove;
        }

        public void SetCanRotateChange()
        {
            CanRotate = !CanRotate;
        }
        #endregion

        #region 인풋값 컨트롤

        int hasAttackCount = Animator.StringToHash("AttactCount");

        public int AttackCount
        {
            get => ANIMATOR.GetInteger(hasAttackCount);
            set => ANIMATOR.SetInteger(hasAttackCount, value);
        }

        void Attack()
        {
            if (CanAttack)
            {
                ANIMATOR.SetTrigger("Attack");
                AttackCount = 0;
            }
        }

        private float horizontalBlend = 0f;
        private float verticalBlend = 0f;

        private void PositionMove()
        {
            if (CanMove)
            {
                ANIMATOR.SetFloat("Magnitude", InputManager.Singletone.MoveDirection.magnitude);

                horizontalBlend = Mathf.Lerp(horizontalBlend, InputManager.Singletone.MoveDirection.x, Time.deltaTime * 10f);
                verticalBlend = Mathf.Lerp(verticalBlend, InputManager.Singletone.MoveDirection.y, Time.deltaTime * 10f);

                ANIMATOR.SetFloat("Horizontal", horizontalBlend);
                ANIMATOR.SetFloat("Vertical", verticalBlend);

                Vector3 adjustMovement = ((transform.forward * InputManager.Singletone.MoveDirection.y) + (transform.right * InputManager.Singletone.MoveDirection.x)).normalized * MoveSpeed * Time.deltaTime;
                CharacterController.Move(adjustMovement);
            }
        }

        private float CameraBottomClamp = -45f; // 카메라가 내려갈 수 있는 최대각도 >> 위를 바라볼 때, 각도
        private float CameraTopClamp = 80f;     // 카메라가 올라갈 수 있는 최대각도 >> 아래를 바라볼 때, 각도
        private float CameraSensitivityX = 1f;  // 마우스 민감도
        private float CameraSensitivityY = 1f;  // 마우스 민감도
        private float CameraTargetYaw;          // 회전값 세로축 (좌 우)
        private float CameraTargetPitch;        // 회전값 가로축 (위 아래)

        void Rotate()
        {
            if (CanRotate)
            {
                float yaw = InputManager.Singletone.MouseMoveDirection.x * rotationSpeed * CameraSensitivityX;
                float pitch = InputManager.Singletone.MouseMoveDirection.y * rotationSpeed * CameraSensitivityY;

                CameraTargetYaw += yaw * Time.deltaTime;
                CameraTargetPitch -= pitch * Time.deltaTime;

                CameraTargetYaw = ClampAngle(CameraTargetYaw, -360, 360);
                CameraTargetPitch = ClampAngle(CameraTargetPitch, CameraBottomClamp, CameraTopClamp);

                transform.rotation = Quaternion.Euler(0, CameraTargetYaw, 0f);
                CameraPivot.transform.rotation = Quaternion.Euler(CameraTargetPitch, CameraTargetYaw, 0f);
            }
        }

        public float ClampAngle(float angle, float min, float max)
        {
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f; // -180 ~ 180도로 정규화
            return Mathf.Clamp(angle, min, max);
        }
        #endregion 
    }
}
