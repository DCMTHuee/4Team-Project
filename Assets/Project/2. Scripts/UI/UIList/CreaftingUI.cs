using UnityEngine;
using UnityEngine.EventSystems;

namespace MoonYoHanStudy
{
    public class CreaftingUI : UIBase
    {
        public void CreaftingButton()
        {
            // �ش� ���� ������Ʈ�� �������ִ� CreaftingSlot ��ũ��Ʈ�� �����´�.

            // ���� Ŭ���� ��ư ������Ʈ ��������
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

            // �ش� ������Ʈ�� �پ� �ִ� CreaftingSlot ��ũ��Ʈ ��������
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
