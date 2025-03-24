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

        void OnMouse(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();
            Vector2 mouseInput = direction * rotationSpeed * Time.deltaTime;

            float currentPitch = mainCameraObject.transform.eulerAngles.x;

            transform.Rotate(Vector3.up * mouseInput.x);

            // ���콺�� ���� �� �ִ� �Ѱ��� ����
            bool isUpMax = false;
            bool isDownMax = false;

            if(30 < currentPitch && currentPitch <= 180)
            {
                isDownMax = true;
            }
            else if (180 <= currentPitch && currentPitch < 330)
            {
                isUpMax = true;
            }

            // ���콺�� �������� ���ٸ� ������ �� �ְ�
            if (mouseInput.y > 0)
            {
                isDownMax = false;
            }
            else if (mouseInput.y < 0)
            {
                isUpMax = false;
            }

            if (!isUpMax && !isDownMax)
            {
                mainCameraObject.transform.Rotate(Vector3.right * -mouseInput.y);
            }
        }
    }
}
