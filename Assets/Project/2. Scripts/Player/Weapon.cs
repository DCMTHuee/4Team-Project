using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace MoonYoHanStudy
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;

        private HashSet<GameObject> alreadyHit = new HashSet<GameObject>(); // 무기에 닿은 오브젝트의 객체를 저장할 변수
        private bool canDamage = false;  // 히트박스 활성화 상태

        public void ClearHashList()
        {
            alreadyHit.Clear(); // 새 공격 시작 → 초기화

            if (alreadyHit.Count == 0)
            {
                CanDamage(false);
            }
        }

        public void CanDamage(bool value)
        {
            canDamage = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Monster") && canDamage)
            {
                if (!alreadyHit.Contains(other.gameObject))
                {
                    Debug.Log("타격 성공");
                    alreadyHit.Add(other.gameObject); // 추가됨으써 더 이상 공격이 들어오지 않음.
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(playerController.AttackPoint);
                }
            }
        }
    }
}
