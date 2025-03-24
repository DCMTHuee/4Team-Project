using UnityEngine;
using Unity.Cinemachine;

namespace MoonYoHanStudy
{
    public class CameraSystem : SingletoneBase<CameraSystem>
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public CinemachineCamera CameraTPS { get; private set; }

        // ī�޶� ���忡�� �����ϰ��ִ� ���� [��ġ ��]
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
            // MainCamera ����, ȭ�� �߾�[Viewport 0.5, 0.5]���� Ray�� ����
            Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            // Raycast�� ����, Ray�� �ε��� ������ AimingPoint�� ����
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, aimingLayerMask, QueryTriggerInteraction.Ignore))
            {
                AimingPoint = hitInfo.point;
            }
            else // Ray �� �ε����� �ʾ��� ���, Ray�� �������� 1000f ��ŭ ������ ������ AimingPoint�� ����
            {
                AimingPoint = ray.GetPoint(1000f);
            }
        }
    }
}
