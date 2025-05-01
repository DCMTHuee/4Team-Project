using UnityEngine;

namespace MoonYoHanStudy
{
    public class CreaftingTest : MonoBehaviour
    {
        public static CreaftingTest Instance;
        private void Awake()
        {
            Instance = this;
        }

        public GameObject BlockOrigin;
        public GameObject BlockApa;

        public float placementDistance = 5f; // ��� ��ġ �Ÿ�

        public LayerMask blockLayer; // ����� ��ġ ������ ���̾�
        public LayerMask RemoveblockLayer; // ���� ������ �� ���̾�

        public bool isBlockMode; // �� ���ɸ��/�Ұ��ɸ�� ��ȯ�� ���� ������
        public bool canBlockInstall; // ��ġ�� �������� Ȯ���ϱ� ���� ������

        public GameObject ApaCube; // � �� ������� Ȯ���ϱ� ���� ������Ʈ

        // int rotateVelue = 0; // �� ȸ���ϱ� ���� ������

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (isBlockMode)
            {

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

        public void BlockModeSwitch()
        {
            isBlockMode = !isBlockMode;
        }
    }
}
