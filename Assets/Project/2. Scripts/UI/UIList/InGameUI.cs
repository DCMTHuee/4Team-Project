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

        bool HideUI = true;

        void OnCreafting()
        {
            GameManager.Instance.SetCursorVisible(HideUI);

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
    }
}
