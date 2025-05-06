using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace MoonYoHanStudy
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;

        private HashSet<GameObject> alreadyHit = new HashSet<GameObject>(); // ���⿡ ���� ������Ʈ�� ��ü�� ������ ����
        private bool canDamage = false;  // ��Ʈ�ڽ� Ȱ��ȭ ����

        public void ClearHashList()
        {
            alreadyHit.Clear(); // �� ���� ���� �� �ʱ�ȭ

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
                    Debug.Log("Ÿ�� ����");
                    alreadyHit.Add(other.gameObject); // �߰������� �� �̻� ������ ������ ����.
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(playerController.AttackPoint);
                }
            }
        }
    }
}
