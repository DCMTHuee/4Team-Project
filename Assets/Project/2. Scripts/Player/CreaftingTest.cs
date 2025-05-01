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

        public float placementDistance = 5f; // 블록 설치 거리

        public LayerMask blockLayer; // 블록이 설치 가능한 레이어
        public LayerMask RemoveblockLayer; // 삭제 가능한 블럭 레이어

        public bool isBlockMode; // 블럭 가능모드/불가능모드 전환을 위한 변수값
        public bool canBlockInstall; // 설치가 가능한지 확인하기 위한 변수값

        public GameObject ApaCube; // 어떤 걸 골랐는지 확인하기 위한 오브젝트

        // int rotateVelue = 0; // 블럭 회전하기 위한 변수값

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

        

        Vector3 SnapToGrid(Vector3 position) // 설치 위치를 고정하기 위해 반올림 처리
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
