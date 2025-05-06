using UnityEngine;
using UnityEngine.EventSystems;

namespace MoonYoHanStudy
{
    public class CreaftingUI : UIBase
    {
        public PlayerController playerController;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerController = GameManager.Singletone.Player;
        }

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
            GameManager.Singletone.SetCursorVisible(false);

            playerController.SetActionSwitch(true, true, true);
        }
    }
}
