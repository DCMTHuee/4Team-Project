using UnityEngine;

namespace MoonYoHanStudy
{
    public class Creafting : MonoBehaviour
    {
        public GameObject blockPrefab; // 블록 프리팹 연결
        public GameObject blockApaPrefab; // 블록알파 프리팹 연결

        public float placementDistance = 5f; // 블록 설치 거리

        public LayerMask blockLayer; // 블록이 설치 가능한 레이어
        public LayerMask RemoveblockLayer; // 삭제 가능한 블럭 레이어

        public bool selectBlock; // 블럭 가능모드/불가능모드 전환을 위한 변수값
        public bool canPlacerBlock; // 설치가 가능한지 확인하기 위한 변수값

        public GameObject ApaCube; // 어떤 걸 골랐는지 확인하기 위한 오브젝트

        int rotateVelue = 0; // 블럭 회전하기 위한 변수값

        void Update()
        {
            SetCursorVisible(isForceCursorVisible);

            // B키를 눌러면 설치 가능 모드, 불가능 모드 전환
            if (Input.GetKeyDown(KeyCode.B))
            {
                selectBlock = !selectBlock; // 설치 가능 모드 변경
                if (ApaCube != null) // 설치 불가능 모드 시, 투명하게 보이던 알파 블럭 삭제
                {
                    Destroy(ApaCube);
                }
            }

            // 설치가 가능한 모드일 경우 활성화
            if (selectBlock)
            {
                PlaceApaBlock();
            }

            // 마우스 오른쪽 클릭 시 블록 제거
            if (Input.GetMouseButtonDown(1))
            {
                RemoveBlock();
            }
        }

        void PlaceApaBlock() // 알파 블럭 설치 하기
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 레이를 쏘고

            if (Physics.Raycast(ray, out RaycastHit hitInfo, placementDistance, blockLayer)) // 레이가 맞으면
            {
                Vector3 placementPosition = hitInfo.point + hitInfo.normal * 0.5f; // 맞은 위치를 확인해서 백터값으로 저장

                placementPosition = SnapToGrid(placementPosition); // 저장한 백터값의 위치를 유니티 에디터 상 Unit기준으로 정리

                if (ApaCube == null) // 만약 블럭이 설치 가능이 확인되지 않는다면
                {
                    canPlacerBlock = true; // 설치 가능하게 만들고
                    ApaCube = Instantiate(blockApaPrefab, placementPosition, Quaternion.Euler(new Vector3(0, rotateVelue, 0))); // 알파 블럭 설치
                }
                else if (ApaCube.gameObject.transform.position != placementPosition) // 만약 화면 전환을 했다면
                {
                    Destroy(ApaCube); // 알파블럭 삭제
                }

                if (Input.GetMouseButtonDown(0) && canPlacerBlock) // 설치가 가능하고, 마우스 클릭 시
                {
                    canPlacerBlock = false; // 동일한 위치에 설치 불가능하게 설치 불가능처리하고 >> 얘는 화면 전환해서 유닛기준으로 값이 바뀌면 다시 활성화가 됨.
                    Instantiate(blockPrefab, placementPosition, Quaternion.Euler(new Vector3(0, rotateVelue, 0))); // 생성
                }
                else if (Input.GetMouseButtonDown(0) && !canPlacerBlock) // 만약 레이쏜 지점에 블럭이 있다면 설치하지 않음.
                {
                    Debug.Log("이미 블록이 있는 위치입니다.");
                }

                if (Input.GetKeyDown(KeyCode.E)) // e키 누를시 블럭 회전
                {
                    rotateVelue += 90;
                    ApaCube.transform.rotation = Quaternion.Euler(new Vector3(0, rotateVelue, 0));
                }
                else if (Input.GetKeyDown(KeyCode.Q)) // q키 누를 시 블럭 회전
                {
                    rotateVelue -= 90;
                    ApaCube.transform.rotation = Quaternion.Euler(new Vector3(0, rotateVelue, 0));
                }
            }
            else
            {
                if (ApaCube != null) // 알파 블럭 설치 가능 여부가 파악된다면 삭제
                {
                    Destroy(ApaCube);
                }
            }
        }

        void RemoveBlock() // 블럭 설치 함수
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // 레이를 쏘고

            if (Physics.Raycast(ray, out RaycastHit hitInfo, placementDistance, RemoveblockLayer)) // 맞으면
            {
                Destroy(hitInfo.collider.gameObject); // 삭제
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

        public bool isForceCursorVisible = false;

        public void SetCursorVisible(bool isVisible)
        {
            Cursor.visible = isVisible || isForceCursorVisible;
            Cursor.lockState = isVisible || isForceCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
