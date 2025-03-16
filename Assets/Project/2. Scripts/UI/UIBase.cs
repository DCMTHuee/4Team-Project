using UnityEngine;

namespace MoonYoHanStudy
{
    public class UIBase : MonoBehaviour
    {
        public virtual void Show(System.Action onShowCallback = null)
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide(System.Action onHideCallback = null)
        {
            gameObject.SetActive(false);
        }
    }
}
