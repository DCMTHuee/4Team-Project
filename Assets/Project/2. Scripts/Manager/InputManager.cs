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

        public event Action MouseLeftPressed;
        public event Action MouseRightPressed;
        public event Action On_T_Key_Pressed;

        void OnPlayerMove(InputValue value)
        {
            MoveDirection = value.Get<Vector2>();
        }

        void OnMouseMove(InputValue value)
        {
            MouseMoveDirection = value.Get<Vector2>();
        }

        void OnMouse_LeftButton()
        {
            MouseLeftPressed?.Invoke();
        }

        void OnMouse_RightButton()
        {
            MouseRightPressed?.Invoke();
            Debug.Log("마우스 우 클릭");
        }

        void OnT_Button()
        {
            On_T_Key_Pressed?.Invoke();
            GameManager.Singletone.SetCursorVisible(true);
            Debug.Log("T 버튼 누름");
        }
    }
}
