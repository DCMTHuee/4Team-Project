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
            HideUI = !HideUI;
            Debug.Log(HideUI);


        }
    }
}
