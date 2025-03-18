using UnityEngine;

namespace MoonYoHanStudy
{
    public class VerticalRotate : MonoBehaviour
    {
        // 카메라의 회전 속도
        public float rotSpeed = 500;

        // 마우스를 내릴 수 있는 한계점 인지
        bool isUpMax;
        bool isDownMax;

        // Update is called once per frame
        void Update()
        {
            // 마우스의 위아래 움직임 저장
            float rotX = Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed;

            // 마우스를 내릴 수 있는 최대 값
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

            // 카메라 자체의 각도만 바꾼다.
            // transform.eulerAngles += new Vector3(-rotX, 0, 0);

            // 누군가의 주변을 도는 변수 / 중간거 백터3로 하면 월드 기준으로 돌아간다.
            if (!isDownMax && !isUpMax)
            {
                transform.RotateAround(transform.parent.position, transform.right, -rotX);
            }
        }
    }
}
