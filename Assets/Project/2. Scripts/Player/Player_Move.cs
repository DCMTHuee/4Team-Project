using UnityEngine;
using UnityEngine.InputSystem;

namespace MoonYoHanStudy
{
    public class Player_Move : MonoBehaviour
    {
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

            float rotationY = direction.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up  * rotationY);
        }
    }
}
