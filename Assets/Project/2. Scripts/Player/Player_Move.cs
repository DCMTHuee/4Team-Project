using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MoonYoHanStudy
{
    public class Player_Move : MonoBehaviour
    {
        [SerializeField] GameObject mainCameraObject;

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 10;

        private CharacterController characterController;

        Vector3 adjustMovement;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                adjustMovement = ((transform.forward * direction.y) + (transform.right * direction.x)).normalized * moveSpeed * Time.deltaTime;
                characterController.Move(adjustMovement);
            }

            if (notPuchAltButton)
            {
                // Rotate(CameraSystem.Singletone.AimingPoint);
            }
        }

        Vector2 direction;
        private bool canMove = true;

        void OnMove(InputValue value)
        {
            direction = value.Get<Vector2>();

            if (direction == Vector2.zero)
            {
                canMove = false;
            }
            else if (direction != Vector2.zero)
            {
                canMove = true;
            }
        }

        bool notPuchAltButton = true;

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

        private float rotationSmoothSpeed = 10;

        void OnMouse(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();

            float yaw = direction.x * rotationSpeed * CameraSensitivityX;
            float pitch = direction.y * rotationSpeed * CameraSensitivityY;

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
            angle = Mathf.Repeat(angle + 180f, 360f) - 180f; // -180 ~ 180도로 정규화
            return Mathf.Clamp(angle, min, max);
        }
    }
}
