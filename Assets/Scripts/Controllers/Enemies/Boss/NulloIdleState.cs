using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NulloIdleState : StateMachineBehaviour
{
    public float m_AttackCooldown;

    Animator m_Animator;
    float m_TimeToNextAttack;
    bool m_Changing;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Animator = animator.gameObject.GetComponent<Animator>();
        m_TimeToNextAttack = m_AttackCooldown;
        m_Changing = false;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_TimeToNextAttack -= Time.deltaTime;

        if (m_TimeToNextAttack <= 0 && !m_Changing)
        {
            int nextAttack = Random.Range(1, 4);
            m_Animator.SetTrigger(GameConstants.ATTACK + nextAttack);
            m_Changing = true;
        }
    }
}
