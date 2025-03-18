using UnityEngine;

namespace MoonYoHanStudy
{
    public class VerticalRotate : MonoBehaviour
    {
        // ī�޶��� ȸ�� �ӵ�
        public float rotSpeed = 500;

        // ���콺�� ���� �� �ִ� �Ѱ��� ����
        bool isUpMax;
        bool isDownMax;

        // Update is called once per frame
        void Update()
        {
            // ���콺�� ���Ʒ� ������ ����
            float rotX = Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

            // ���콺�� ���� �� �ִ� �ִ� ��
            if (transform.eulerAngles.x >= 70 && transform.eulerAngles.x <= 180)
            {
                isDownMax = true;
            }
            else if (transform.eulerAngles.x <= 320 && transform.eulerAngles.x > 180)
            {
                isUpMax = true;
            }

            if (rotX > 0)
            {
                isDownMax = false;
            }
            else if (rotX < 0)
            {
                isUpMax = false;
            }

            // ī�޶� ��ü�� ������ �ٲ۴�.
            // transform.eulerAngles += new Vector3(-rotX, 0, 0);

            // �������� �ֺ��� ���� ���� / �߰��� ����3�� �ϸ� ���� �������� ���ư���.
            if (!isDownMax && !isUpMax)
            {
                transform.RotateAround(transform.parent.position, transform.right, -rotX);
            }
        }
    }
}
