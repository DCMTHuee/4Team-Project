using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MoonYoHanStudy
{
    public class InputManager : SingletoneBase<InputManager, DontDestroyOnLoadOff>
    {
        public Vector2 MoveDirection;
        public Vector2 MouseMoveDirection;

        // public event Action MouseLeftPressed;
        public event Action On_T_Key_Pressed;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMove(InputValue value)
        {
            MoveDirection = value.Get<Vector2>();
        }

        void OnMouse(InputValue value)
        {
            MouseMoveDirection = value.Get<Vector2>();
        }

        void OnCreafting()
        {
            On_T_Key_Pressed?.Invoke();
            GameManager.Singletone.SetCursorVisible(true);
            Debug.Log("T 버튼 누름");
        }
    }
}
