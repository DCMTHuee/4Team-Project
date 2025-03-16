using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class UIManager : SingletoneBase<UIManager>
    {
        public static T Show<T>(UIList uiPrefabName, System.Action onShowCallback = null) where T : UIBase
        {
            var newUI = Singletone.GetUI<T>(uiPrefabName);

            if (newUI == null)
            {
                return null;
            }

            newUI.Show(onShowCallback);

            return newUI;
        }

        public static T Hide<T>(UIList uiPrefabName, System.Action onHideCallback = null) where T : UIBase
        {
            var targetUI = Singletone.GetUI<T>(uiPrefabName);

            if (targetUI == null)
            {
                return null;
            }

            targetUI.Hide(onHideCallback);

            return targetUI;
        }

        [SerializeField] private Transform popupRoot;
        [SerializeField] private Transform panelRoot;

        private Dictionary<UIList, UIBase> popups = new Dictionary<UIList, UIBase>();
        private Dictionary<UIList, UIBase> panels = new Dictionary<UIList, UIBase>();


        private const string UI_PREFAB_PATH = "UI/Prefab/";

        public void Initialized()
        {
            if (popupRoot == null)
            {
                GameObject popupObject = new GameObject("Popup Root");
                popupRoot = popupObject.transform;
                popupRoot.SetParent(transform);
                popupRoot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            if (panelRoot == null)
            {
                GameObject panelObject = new GameObject("Panel Root");
                panelRoot = panelObject.transform;
                panelRoot.SetParent(transform);
                panelRoot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            for (int index = (int)UIList.POPUP_START + 1; index < (int)UIList.POPUP_END; index++)
            {
                popups.Add((UIList)index, null);
            }

            for (int index = (int)UIList.PANEL_START + 1; index < (int)UIList.PANEL_END; index++)
            {
                panels.Add((UIList)index, null);
            }
        }

        public bool GetUI<T>(UIList uiPrefabName, out T ui, bool reload = false) where T : UIBase
        {
            ui = GetUI<T>(uiPrefabName, reload);
            return ui != null;
        }

        public T GetUI<T>(UIList uiPrefabName, bool reload = false) where T : UIBase
        {
            Dictionary<UIList, UIBase> container =
                uiPrefabName > UIList.POPUP_START && uiPrefabName < UIList.POPUP_END ? popups : panels;

            Transform root =
                uiPrefabName > UIList.POPUP_START && uiPrefabName < UIList.POPUP_END ? popupRoot : panelRoot;

            if (!container.ContainsKey(uiPrefabName))
            {
                return null;
            }

            if (reload && container[uiPrefabName] != null)
            {
                Destroy(container[uiPrefabName].gameObject);
                container[uiPrefabName] = null;
            }

            if (!container[uiPrefabName])
            {
                string path = $"UI.{uiPrefabName}";
                UIBase loadedPrefab = Resources.Load<UIBase>(UI_PREFAB_PATH + path);
                var newCreatedUI = Instantiate(loadedPrefab, root);

                if (newCreatedUI)
                {
                    container[uiPrefabName] = newCreatedUI.GetComponent<T>();

                    if (container[uiPrefabName])
                    {
                        container[uiPrefabName].gameObject.SetActive(false);
                    }
                }
            }

            return (T)container[uiPrefabName];
        }
    }
}
