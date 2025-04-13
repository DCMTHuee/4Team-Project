using UnityEngine;

namespace MoonYoHanStudy
{
    public class InGameUI : UIBase
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {
        
        }

        bool HideUI = false;

        void OnCreafting()
        {
            HideUI = !HideUI;

            if (HideUI)
            {
                UIManager.Show<CreaftingUI>(UIList.CreaftingUI);
            }
            else if (!HideUI)
            {
                UIManager.Hide<CreaftingUI>(UIList.CreaftingUI);
            }

            GameManager.Instance.SetCursorVisible(HideUI);
        }
    }
}
