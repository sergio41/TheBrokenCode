using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateMachineBehaviour
{
    public float m_TimeWaiting;

    Animator m_Animator;
    float m_TimeWaitingLeft;
    bool m_Changing = false;
    EnemyFollowerController m_EnemyFollower;
    EnemyAttackerController m_EnemyAttacker;
    Transform m_Player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var m_Rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        m_Animator = animator.gameObject.GetComponent<Animator>();
        m_EnemyFollower = animator.gameObject.GetComponent<EnemyFollowerController>();
        m_EnemyAttacker = animator.gameObject.GetComponent<EnemyAttackerController>();
        m_Player = FindObjectOfType<FixeriaHealth>().m_FixeriaCenter;
        m_TimeWaitingLeft = m_TimeWaiting;
        m_Changing = false;
        m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_TimeWaitingLeft -= Time.deltaTime;

        if (m_TimeWaitingLeft <= 0 && !m_Changing)
        {
            m_Animator.SetBool(GameConstants.IS_RUN, true);
            m_Animator.SetInteger(GameConstants.DIRECTION, m_Animator.GetInteger(GameConstants.DIRECTION) * -1);
            m_Changing = true;
        }

        if (m_EnemyAttacker != null && m_EnemyAttacker.ReadyForAttack() &&
            RaycastUtils.IsVisible(m_EnemyAttacker.m_FireOrigin, m_EnemyAttacker.transform.right * -1, m_Player, m_EnemyAttacker.m_AngleDetected, m_EnemyAttacker.m_DistanceToAttack, GameConstants.PLAYER, new List<string> { GameConstants.ENEMY, GameConstants.SPELL, GameConstants.SPELL_IGNORE }))
        {
            m_EnemyAttacker.AttackDone();
            m_Animator.SetTrigger(GameConstants.ATTACK);
            m_Changing = true;
        }
        else if (m_EnemyFollower != null &&
            RaycastUtils.IsVisible(m_EnemyFollower.m_RayOrigin, m_EnemyFollower.transform.right * -1, m_Player, m_EnemyFollower.m_AngleDetected, m_EnemyFollower.m_DistanceToFollow, GameConstants.PLAYER, new List<string> { GameConstants.ENEMY, GameConstants.SPELL, GameConstants.SPELL_IGNORE }))
        {
            m_Animator.SetBool(GameConstants.IS_FOLLOW, true);
            m_Changing = true;
        }
    }
}
