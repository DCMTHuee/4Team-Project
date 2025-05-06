using UnityEngine;

namespace MoonYoHanStudy
{
    public class CanMove : StateMachineBehaviour
    {
        [SerializeField] string triggerName;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.gameObject.GetComponent<PlayerController>().SetActionSwitch(true, true, true);
        }
    }
}
