using UnityEngine;

namespace MoonYoHanStudy
{
    public class Creafting : MonoBehaviour
    {
        public GameObject blockPrefab; // ��� ������ ����
        public GameObject blockApaPrefab; // ��Ͼ��� ������ ����

        public float placementDistance = 5f; // ��� ��ġ �Ÿ�

        public LayerMask blockLayer; // ����� ��ġ ������ ���̾�
        public LayerMask RemoveblockLayer; // ���� ������ �� ���̾�

        public bool selectBlock; // �� ���ɸ��/�Ұ��ɸ�� ��ȯ�� ���� ������
        public bool canPlacerBlock; // ��ġ�� �������� Ȯ���ϱ� ���� ������

        public GameObject ApaCube; // � �� ������� Ȯ���ϱ� ���� ������Ʈ

        int rotateVelue = 0; // �� ȸ���ϱ� ���� ������

        void Update()
        {
            SetCursorVisible(isForceCursorVisible);

            // BŰ�� ������ ��ġ ���� ���, �Ұ��� ��� ��ȯ
            if (Input.GetKeyDown(KeyCode.B))
            {
                selectBlock = !selectBlock; // ��ġ ���� ��� ����
                if (ApaCube != null) // ��ġ �Ұ��� ��� ��, �����ϰ� ���̴� ���� �� ����
                {
                    Destroy(ApaCube);
                }
            }

            // ��ġ�� ������ ����� ��� Ȱ��ȭ
            if (selectBlock)
            {
                PlaceApaBlock();
            }

            // ���콺 ������ Ŭ�� �� ��� ����
            if (Input.GetMouseButtonDown(1))
            {
                RemoveBlock();
            }
        }

        void PlaceApaBlock() // ���� �� ��ġ �ϱ�
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // ���̸� ���

            if (Physics.Raycast(ray, out RaycastHit hitInfo, placementDistance, blockLayer)) // ���̰� ������
            {
                Vector3 placementPosition = hitInfo.point + hitInfo.normal * 0.5f; // ���� ��ġ�� Ȯ���ؼ� ���Ͱ����� ����

                placementPosition = SnapToGrid(placementPosition); // ������ ���Ͱ��� ��ġ�� ����Ƽ ������ �� Unit�������� ����

                if (ApaCube == null) // ���� ���� ��ġ ������ Ȯ�ε��� �ʴ´ٸ�
                {
                    canPlacerBlock = true; // ��ġ �����ϰ� �����
                    ApaCube = Instantiate(blockApaPrefab, placementPosition, Quaternion.Euler(new Vector3(0, rotateVelue, 0))); // ���� �� ��ġ
                }
                else if (ApaCube.gameObject.transform.position != placementPosition) // ���� ȭ�� ��ȯ�� �ߴٸ�
                {
                    Destroy(ApaCube); // ���ĺ� ����
                }

                if (Input.GetMouseButtonDown(0) && canPlacerBlock) // ��ġ�� �����ϰ�, ���콺 Ŭ�� ��
                {
                    canPlacerBlock = false; // ������ ��ġ�� ��ġ �Ұ����ϰ� ��ġ �Ұ���ó���ϰ� >> ��� ȭ�� ��ȯ�ؼ� ���ֱ������� ���� �ٲ�� �ٽ� Ȱ��ȭ�� ��.
                    Instantiate(blockPrefab, placementPosition, Quaternion.Euler(new Vector3(0, rotateVelue, 0))); // ����
                }
                else if (Input.GetMouseButtonDown(0) && !canPlacerBlock) // ���� ���̽� ������ ���� �ִٸ� ��ġ���� ����.
                {
                    Debug.Log("�̹� ����� �ִ� ��ġ�Դϴ�.");
                }

                if (Input.GetKeyDown(KeyCode.E)) // eŰ ������ �� ȸ��
                {
                    rotateVelue += 90;
                    ApaCube.transform.rotation = Quaternion.Euler(new Vector3(0, rotateVelue, 0));
                }
                else if (Input.GetKeyDown(KeyCode.Q)) // qŰ ���� �� �� ȸ��
                {
                    rotateVelue -= 90;
                    ApaCube.transform.rotation = Quaternion.Euler(new Vector3(0, rotateVelue, 0));
                }
            }
            else
            {
                if (ApaCube != null) // ���� �� ��ġ ���� ���ΰ� �ľǵȴٸ� ����
                {
                    Destroy(ApaCube);
                }
            }
        }

        void RemoveBlock() // �� ��ġ �Լ�
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // ���̸� ���

            if (Physics.Raycast(ray, out RaycastHit hitInfo, placementDistance, RemoveblockLayer)) // ������
            {
                Destroy(hitInfo.collider.gameObject); // ����
            }
        }

        Vector3 SnapToGrid(Vector3 position) // ��ġ ��ġ�� �����ϱ� ���� �ݿø� ó��
        {
            return new Vector3(
                Mathf.Round(position.x),
                Mathf.Round(position.y),
                Mathf.Round(position.z)
            );
        }

        public bool isForceCursorVisible = false;

        public void SetCursorVisible(bool isVisible)
        {
            Cursor.visible = isVisible || isForceCursorVisible;
            Cursor.lockState = isVisible || isForceCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
