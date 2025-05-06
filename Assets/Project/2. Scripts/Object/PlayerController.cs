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
        [SerializeField] private CharacterController characterController; // 캐릭터 컨트롤러

        [SerializeField] private GameObject playerController;

        float currentHP; // 현재 체력
        float currentST; // 현재 스테미너

        float moveSpeed; // 이동 스피드
        [SerializeField] float rotationSpeed = 10; // 카메라 회전력

        bool canMove = true;
        bool canRotate = true;

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

            moveSpeed = data.Speed;

            ANIMATOR = GetComponent<Animator>();

            InputManager.Singletone.MouseLeftPressed += Attack;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            PositionMove();

            Rotate();

            currentState?.InvokeOnUpdate();
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
            currentST -= amount;
        }

        public float GetCurrnetHP()
        {
            return currentHP;
        }

        public float GetCurrnetST()
        {
            return currentST;
        }

        public void SetCanMoveSwitch(bool true_false)
        {
            canMove = true_false;
        }

        public void SetCanRotateSwitch(bool true_false)
        {
            canRotate = true_false;
        }

        public void SetCanMoveChange()
        {
            canMove = !canMove;
        }

        public void SetCanRotateChange()
        {
            canRotate = !canRotate;
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
            ANIMATOR.SetTrigger("Attack");
            AttackCount = 0;
        }

        private float horizontalBlend = 0f;
        private float verticalBlend = 0f;

        private void PositionMove()
        {
            if (canMove)
            {
                ANIMATOR.SetFloat("Magnitude", InputManager.Singletone.MoveDirection.magnitude);

                horizontalBlend = Mathf.Lerp(horizontalBlend, InputManager.Singletone.MoveDirection.x, Time.deltaTime * 10f);
                verticalBlend = Mathf.Lerp(verticalBlend, InputManager.Singletone.MoveDirection.y, Time.deltaTime * 10f);

                ANIMATOR.SetFloat("Horizontal", horizontalBlend);
                ANIMATOR.SetFloat("Vertical", verticalBlend);

                Vector3 adjustMovement = ((transform.forward * InputManager.Singletone.MoveDirection.y) + (transform.right * InputManager.Singletone.MoveDirection.x)).normalized * moveSpeed * Time.deltaTime;
                characterController.Move(adjustMovement);
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
            if (canRotate)
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
