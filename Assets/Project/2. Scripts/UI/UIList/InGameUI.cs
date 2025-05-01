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

        // Update is called once per frame
        void Update()
        {
                
        }

        public void HP()
        {
            HP_bar.localScale = new Vector3(playerController.GetCurrnetHP() / playerController.MaxHP, 1, 1);
            ST_bar.localScale = new Vector3(playerController.GetCurrnetST() / playerController.MaxST, 1, 1);
        }


        float Compass_Material_Offset = 0;

        void Compass()
        {
            Compass_Material_Offset += Time.deltaTime;

            Compass_Material.material.mainTextureOffset = new Vector2(Compass_Material_Offset, 0);

            if (Compass_Material_Offset >= 10)
            {
                Compass_Material_Offset = 0;
            }
        }


        bool HideUI = true;

        void OnCreafting()
        {
            GameManager.Singletone.SetCursorVisible(HideUI);

            HideUI = !HideUI;

            if (HideUI)
            {
                UIManager.Hide<CreaftingUI>(UIList.CreaftingUI);
            }
            else
            {
                UIManager.Show<CreaftingUI>(UIList.CreaftingUI);
            }
        }

        public void Esc()
        {
            Bigin.Singletone.ChangeScene(SceneType.Title);
        }
    }
}
