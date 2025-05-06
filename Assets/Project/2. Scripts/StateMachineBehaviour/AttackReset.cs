using Unity.VisualScripting;
using UnityEngine;

namespace MoonYoHanStudy
{
    public class AttackReset : StateMachineBehaviour
    {
        [SerializeField] string triggerName;
        private Weapon hitbox;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (hitbox == null)
            {
                hitbox = animator.GetComponentInChildren<Weapon>(true);
            }

            hitbox.CanDamage(true);

            animator.gameObject.GetComponent<PlayerController>().SetActionSwitch(false, true, true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(triggerName);

            hitbox.ClearHashList();

            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

            if (state.IsTag("Attack"))
            {
                // 현재 애니메이션이 공격 중이다
                hitbox.CanDamage(true);
                return;
            }
        }
    }
}
