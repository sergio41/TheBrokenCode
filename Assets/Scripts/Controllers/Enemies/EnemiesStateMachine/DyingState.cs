using UnityEngine;

public class DyingState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0, animator.GetComponent<Rigidbody2D>().velocity.y);
    }
}
