using UnityEngine;
using UnityEngine.InputSystem;

namespace MoonYoHanStudy
{
    public class Player_Move : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;

        private CharacterController characterController;

        private bool canMove = true;



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }


        Vector3 adjustMovement;

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                characterController.Move(adjustMovement);
            }
        }


        void OnMove(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();

            if (direction == Vector2.zero)
            {
                canMove = false;
            }
            else if (direction != Vector2.zero)
            {
                canMove = true;

                Vector3 movement = (transform.forward * direction.y) + (transform.right * direction.x);
                adjustMovement = movement * moveSpeed * Time.deltaTime;
            }
        }


        [SerializeField] float rotationSpeed = 10;

        void OnMouse(InputValue value)
        {
            Vector2 direction = value.Get<Vector2>();

            float rotationY = direction.x * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotationY, 0f);
        }
    }
}
