using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace MoonYoHanStudy
{
    public class PlayerController : ObjectStatusBase
    {
        [SerializeField] private PlayerType playerType;
        [SerializeField] private PlayerData playerData; // �÷��̾� ������
        [SerializeField] private GameObject CameraPivot; // ���� ī�޶�
        [SerializeField] private CharacterController characterController; // ĳ���� ��Ʈ�ѷ�

        [SerializeField] private GameObject playerController;

        float currentHP; // ���� ü��
        float currentST; // ���� ���׹̳�

        float moveSpeed; // �̵� ���ǵ�
        [SerializeField] float rotationSpeed = 10; // ī�޶� ȸ����

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
                Debug.Log("���Ͱ� ����");
            }
        }

        #region Get/Set�� Switch/Change

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

        #region ��ǲ�� ��Ʈ��

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

        private float CameraBottomClamp = -45f; // ī�޶� ������ �� �ִ� �ִ밢�� >> ���� �ٶ� ��, ����
        private float CameraTopClamp = 80f;     // ī�޶� �ö� �� �ִ� �ִ밢�� >> �Ʒ��� �ٶ� ��, ����
        private float CameraSensitivityX = 1f;  // ���콺 �ΰ���
        private float CameraSensitivityY = 1f;  // ���콺 �ΰ���
        private float CameraTargetYaw;          // ȸ���� ������ (�� ��)
        private float CameraTargetPitch;        // ȸ���� ������ (�� �Ʒ�)

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
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f; // -180 ~ 180���� ����ȭ
            return Mathf.Clamp(angle, min, max);
        }
        #endregion 
    }
}
