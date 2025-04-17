using UnityEngine;
using UnityEngine.EventSystems;

namespace MoonYoHanStudy
{
    public class CreaftingUI : UIBase
    {
        public void CreaftingButton()
        {
            // 해당 게임 오브젝트가 가지고있는 CreaftingSlot 스크립트를 가져온다.

            // 현재 클릭된 버튼 오브젝트 가져오기
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

            // 해당 오브젝트에 붙어 있는 CreaftingSlot 스크립트 가져오기
            CreaftingSlot creaftingSlot = clickedObject.GetComponent<CreaftingSlot>();

            CreaftingTest.Instance.BlockModeSwitch();

            CreaftingTest.Instance.BlockOrigin = creaftingSlot.BlockOrigin;
            CreaftingTest.Instance.BlockApa = creaftingSlot.BlockApa;
        }

        public void HideUI()
        {
            UIManager.Hide<CreaftingUI>(UIList.CreaftingUI);
            GameManager.Instance.SetCursorVisible(false);
        }
    }
}
