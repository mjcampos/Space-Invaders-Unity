using UnityEngine;

public class EndShootTrigger : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SendMessage("NoLongerShooting");
    }
}
