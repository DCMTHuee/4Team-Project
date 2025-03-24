using UnityEngine;
using Unity.Cinemachine;

namespace MoonYoHanStudy
{
    public class CameraSystem : SingletoneBase<CameraSystem>
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public CinemachineCamera CameraTPS { get; private set; }

        // 카메라 입장에서 조준하고있는 지점 [위치 값]
        public Vector3 AimingPoint { get; private set; }

        [SerializeField] private LayerMask aimingLayerMask;
        private CinemachineThirdPersonFollow tpsCameraFollow;
        private bool isRightSide = true;
        private float targetCameraSide = 1f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            tpsCameraFollow = CameraTPS.GetComponent<CinemachineThirdPersonFollow>();
        }

        // Update is called once per frame
        void Update()
        {
            // MainCamera 기준, 화면 중앙[Viewport 0.5, 0.5]에서 Ray를 생성
            Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            // Raycast를 통해, Ray가 부딪힌 지점을 AimingPoint에 저장
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, aimingLayerMask, QueryTriggerInteraction.Ignore))
            {
                AimingPoint = hitInfo.point;
            }
            else // Ray 가 부딪히지 않았을 경우, Ray의 방향으로 1000f 만큼 떨어진 지점을 AimingPoint에 저장
            {
                AimingPoint = ray.GetPoint(1000f);
            }
        }
    }
}
