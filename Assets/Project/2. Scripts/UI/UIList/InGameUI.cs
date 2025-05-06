using System;
using UnityEngine;
using UnityEngine.UI;

namespace MoonYoHanStudy
{
    public class InGameUI : UIBase
    {
        public PlayerController playerController;

        public Image Compass_Material;

        public RectTransform HP_bar;
        public RectTransform ST_bar;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerController = GameManager.Singletone.Player;
        }

        private void OnEnable()
        {
            InputManager.Singletone.On_T_Key_Pressed += OnCreafting;
        }

        private void OnDisable()
        {
            InputManager.Singletone.On_T_Key_Pressed -= OnCreafting;
        }

        private void Update()
        {
            HP();
            Compass();
        }

        public void HP()
        {
            float currentHP = playerController.GetCurrnetHP();
            currentHP = Mathf.Clamp(currentHP, 0, playerController.MaxHP);
            HP_bar.localScale = new Vector3(currentHP / playerController.MaxHP, 1, 1);

            ST_bar.localScale = new Vector3(playerController.GetCurrnetST() / playerController.MaxST, 1, 1);
        }


        float Compass_Material_Offset = 0;

        void Compass()
        {
            Compass_Material_Offset += InputManager.Singletone.MouseMoveDirection.x;

            if (Compass_Material_Offset >= 360)
            {
                Compass_Material_Offset = 0;
            }

            Compass_Material.material.mainTextureOffset = new Vector2(Compass_Material_Offset / 360, 0);
        }

        public void OnCreafting()
        {
            if (UIManager.activePopupsUI[UIList.CreaftingUI])
            {
                UIManager.Hide<CreaftingUI>(UIList.CreaftingUI);
                GameManager.Singletone.SetCursorVisible(false);

                playerController.SetActionSwitch(true, true, true);
            }
            else
            {
                UIManager.Show<CreaftingUI>(UIList.CreaftingUI);
                GameManager.Singletone.SetCursorVisible(true);

                playerController.SetActionSwitch(false, false, false);
            }
        }

        public void Esc()
        {
            Bigin.Singletone.ChangeScene(SceneType.Title);
        }
    }
}
