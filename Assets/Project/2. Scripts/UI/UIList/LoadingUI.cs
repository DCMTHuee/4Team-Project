using UnityEngine;
using UnityEngine.UI;

namespace MoonYoHanStudy
{
    public class LoadingUI : UIBase
    {
        public Sprite[] image;
        public Image panel_image;

        private void OnEnable()
        {
            Sprit_Change();
        }

        void Sprit_Change()
        {
            if ( Bigin.Singletone.isStart)
            {
                panel_image.sprite = image[Random.Range(1, image.Length - 1)];
            }
            else
            {
                panel_image.sprite = image[0];
            }
        }
    }
}
