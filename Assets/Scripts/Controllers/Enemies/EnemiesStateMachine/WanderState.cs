using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : StateMachineBehaviour
{
    public LayerMask m_GroundMask;
    public float m_CheckRadius;
    public float m_Speed;
    public float m_TimeWandering;

    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    int m_Direction = -1;
    float m_TimeWanderingLeft;
    Transform m_Checker;
    bool m_Changing = false;
    EnemyFollowerController m_EnemyFollower;
    EnemyAttackerController m_EnemyAttacker;
    Transform m_Player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Checker = animator.gameObject.GetComponent<EnemyBasicController>().m_Checker;
        m_Animator = animator.gameObject.GetComponent<Animator>();
        m_Rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        m_EnemyFollower = animator.gameObject.GetComponent<EnemyFollowerController>();
        m_EnemyAttacker = animator.gameObject.GetComponent<EnemyAttackerController>();
        m_Player = FindObjectOfType<FixeriaHealth>().m_FixeriaCenter;
        m_TimeWanderingLeft = m_TimeWandering;
        m_Direction = animator.GetInteger(GameConstants.DIRECTION);
        m_Changing = false;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_TimeWanderingLeft -= Time.deltaTime;
        if (!m_Changing)
        {
            if (m_Direction > 0)
                animator.gameObject.transform.eulerAngles = new Vector2(0, 180);
            else if (m_Direction < 0)
                animator.gameObject.transform.eulerAngles = new Vector2(0, 0);


            var isOutOfBounds = !Physics2D.OverlapCircle(m_Checker.position, m_CheckRadius, m_GroundMask);
            if (isOutOfBounds || m_TimeWanderingLeft <= 0)
            {
                m_Animator.SetBool(GameConstants.IS_RUN, false);
                m_Changing = true;
            }
            else
            {
                m_Rigidbody.velocity = new Vector2(m_Direction * m_Speed, m_Rigidbody.velocity.y);
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
}
