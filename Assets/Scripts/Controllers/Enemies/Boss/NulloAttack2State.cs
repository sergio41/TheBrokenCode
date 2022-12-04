using UnityEngine;

public class NulloAttack2State : StateMachineBehaviour
{
    public float m_WaitUntilAttack;
    public AudioClip m_Attack;

    float m_TimeLeftToAttack;
    bool m_Attacking;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Attacking = false;
        m_TimeLeftToAttack = m_WaitUntilAttack;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_TimeLeftToAttack -= Time.deltaTime;

        if (m_TimeLeftToAttack <= 0 && !m_Attacking)
        {
            animator.gameObject.GetComponent<AudioSource>().PlayOneShot(m_Attack);
            m_Attacking = true;
        }
    }
}