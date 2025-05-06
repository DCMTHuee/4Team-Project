using UnityEngine;

namespace MoonYoHanStudy
{
    public class EnemyCanvas : MonoBehaviour
    {
        [SerializeField] Transform player;

        private void Start()
        {
            // player = GameManager.Singletone.Player.transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(player.transform);
        }
    }
}
