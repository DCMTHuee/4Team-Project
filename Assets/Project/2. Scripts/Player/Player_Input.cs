using UnityEngine;

namespace MoonYoHanStudy
{
    public class Player_Input : MonoBehaviour
    {
        [HideInInspector] public static Player_Input player_Input = null;

        bool CreaftingUIButton = false;

        private void Awake()
        {
            player_Input = this;
        }

        void OnCreafting()
        {
            Debug.Log("클릭 체크");
            CreaftingUIButton = !CreaftingUIButton;

            if (CreaftingUIButton)
            {
                UIManager.Show<CreaftingUI>(UIList.CreaftingUI);
            }
            else
            {
                UIManager.Hide<CreaftingUI>(UIList.CreaftingUI);
            }

            // CreaftingUIButton = CreaftingUIButton ? UIManager.Show<CreaftingUI>(UIList.CreaftingUI) : UIManager.Hide<CreaftingUI>(UIList.CreaftingUI);
        }
    }
}
