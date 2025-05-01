using UnityEngine;
using UnityEngine.InputSystem;

namespace MoonYoHanStudy
{
    public class PlayerController : ObjectStatusBase
    {
        [SerializeField] private PlayerType playerType;
        [SerializeField] private PlayerData playerData; // �÷��̾� ������
        [SerializeField] private GameObject mainCameraObject; // ���� ī�޶�
        [SerializeField] private CharacterController characterController; // ĳ���� ��Ʈ�ѷ�

        float currentHP; // ���� ü��
        float currentST; // ���� ���׹̳�

        float moveSpeed; // �̵� ���ǵ�
        [SerializeField] float rotationSpeed = 10; // ī�޶� ȸ����

        // Vector3 adjustMovement; // �̵��ؾ��� ����

        // bool canAction = true;
        bool canMove = true;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Init();
        }

        void Init()
        {
            var data = playerData.GetData(playerType);

            MaxHP = data.MaxHP;
            currentHP = MaxHP;

            MaxST = data.MaxST;
            currentST = MaxST;

            moveSpeed = data.Speed;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            Debug.Log(InputManager.Singletone.MoveDirection);
            Debug.Log(InputManager.Singletone.MouseMoveDirection);

            if (InputManager.Singletone.MoveDirection == Vector2.zero)
            {
                canMove = false;
                // Debug.Log("canMove ����");
            }
            else if (InputManager.Singletone.MoveDirection != Vector2.zero)
            {
                canMove = true;
                // Debug.Log("canMove ��");
            }

            if (canMove)
            {
                Vector3 adjustMovement = ((transform.forward * InputManager.Singletone.MoveDirection.y) + (transform.right * InputManager.Singletone.MoveDirection.x)).normalized * moveSpeed * Time.deltaTime;
                characterController.Move(adjustMovement);
            }

            Rotate();
        }

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

        #region ��ǲ�� ��Ʈ�� // ���߿� ���� ���

        // bool notPuchAltButton = true;

        public void Rotate(Vector3 targetAimPoint)
        {
            Vector3 aimTarget = targetAimPoint;
            aimTarget.y = transform.position.y;
            Vector3 pos = transform.position;

            Vector3 aimDirection = (aimTarget - pos).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 10f);
        }

        private float CameraBottomClamp = -45f;
        private float CameraTopClamp = 80f;
        private float CameraSensitivityX = 1f;
        private float CameraSensitivityY = 1f;
        private float CameraTargetYaw;
        private float CameraTargetPitch;

        // private float rotationSmoothSpeed = 10;

        void Rotate()
        {
            float yaw = InputManager.Singletone.MouseMoveDirection.x * rotationSpeed * CameraSensitivityX;
            float pitch = InputManager.Singletone.MouseMoveDirection.y * rotationSpeed * CameraSensitivityY;

            CameraTargetYaw += yaw * Time.deltaTime;
            CameraTargetPitch -= pitch * Time.deltaTime;

            CameraTargetYaw = ClampAngle(CameraTargetYaw, -360, 360);
            CameraTargetPitch = ClampAngle(CameraTargetPitch, CameraBottomClamp, CameraTopClamp);

            transform.rotation = Quaternion.Euler(0, CameraTargetYaw, 0f);
            mainCameraObject.transform.rotation = Quaternion.Euler(CameraTargetPitch, CameraTargetYaw, 0f);



            /*            Quaternion targetRotation = Quaternion.Euler(CameraTargetPitch, CameraTargetYaw, 0f);
                        mainCameraObject.transform.rotation = Quaternion.Slerp(mainCameraObject.transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);*/
        }

        public float ClampAngle(float angle, float min, float max)
        {
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f; // -180 ~ 180���� ����ȭ
            return Mathf.Clamp(angle, min, max);
        }

        #endregion 
    }
}
