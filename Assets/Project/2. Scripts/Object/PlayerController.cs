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

        [SerializeField] private Skill skill;

        [SerializeField] float currentHP; // ���� ü��
        public RectTransform HP_bar;
        [SerializeField] float currentST; // ���� ���׹̳�

        [SerializeField] float rotationSpeed = 10; // ī�޶� ȸ����

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
                Debug.Log("���Ͱ� ����");
            }
        }

        #region Get/Set�� Switch/Change

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

        #region ��ǲ�� ��Ʈ��

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

        private float CameraBottomClamp = -45f; // ī�޶� ������ �� �ִ� �ִ밢�� >> ���� �ٶ� ��, ����
        private float CameraTopClamp = 80f;     // ī�޶� �ö� �� �ִ� �ִ밢�� >> �Ʒ��� �ٶ� ��, ����
        private float CameraSensitivityX = 1f;  // ���콺 �ΰ���
        private float CameraSensitivityY = 1f;  // ���콺 �ΰ���
        private float CameraTargetYaw;          // ȸ���� ������ (�� ��)
        private float CameraTargetPitch;        // ȸ���� ������ (�� �Ʒ�)

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
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f; // -180 ~ 180���� ����ȭ
            return Mathf.Clamp(angle, min, max);
        }
        #endregion 
    }
}
