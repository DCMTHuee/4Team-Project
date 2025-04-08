using UnityEngine;
using UnityEngine.UI;

namespace MoonYoHanStudy
{
    public class StatusIconManager : MonoBehaviour
    {
        public static StatusIconManager Instance;

        [SerializeField] private GameObject iconPrefab;
        [SerializeField] private Sprite stunIcon;
        [SerializeField] private Sprite bleedIcon;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowIcon(GameObject uiRoot, StatusEffect effect)
        {
            var icon = Instantiate(iconPrefab, uiRoot.transform);
            icon.GetComponent<Image>().sprite = GetIcon(effect);
            icon.name = effect.ToString();
        }

        public void RemoveIcon(GameObject uiRoot, StatusEffect effect)
        {
            var child = uiRoot.transform.Find(effect.ToString());
            if (child != null)
                Destroy(child.gameObject);
        }

        Sprite GetIcon(StatusEffect effect)
        {
            return effect switch
            {
                StatusEffect.Stunned => stunIcon,
                StatusEffect.Burning => bleedIcon,
                _ => null
            };
        }
    }
}
