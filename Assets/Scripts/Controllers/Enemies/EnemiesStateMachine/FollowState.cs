using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : StateMachineBehaviour
{
    public float m_Speed;
    public bool m_Flying = false;
    public AudioClip m_FollowSound;

    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    bool m_Changing = false;
    EnemyFollowerController m_EnemyFollower;
    EnemyAttackerController m_EnemyAttacker;
    Transform m_Player;
    float m_TimeToNextAttack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Animator = animator.gameObject.GetComponent<Animator>();
        m_Rigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
        m_EnemyFollower = animator.gameObject.GetComponent<EnemyFollowerController>();
        m_EnemyAttacker = animator.gameObject.GetComponent<EnemyAttackerController>();
        m_Player = FindObjectOfType<FixeriaHealth>().m_FixeriaCenter;
        m_Changing = false;
        animator.gameObject.GetComponent<AudioSource>().PlayOneShot(m_FollowSound);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!m_Changing)
        {
            var m_Direction = 1;
            if (m_Player.position.x < animator.transform.position.x)
                m_Direction = -1;

            if (m_Direction > 0)
                animator.gameObject.transform.eulerAngles = new Vector2(0, 180);
            else if (m_Direction < 0)
                animator.gameObject.transform.eulerAngles = new Vector2(0, 0);

            if (!m_Flying)
                m_Rigidbody.velocity = new Vector2(m_Direction * m_Speed, m_Rigidbody.velocity.y);
            else
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, m_Player.position, m_Speed * Time.deltaTime);

            if (m_TimeToNextAttack > 0)
                m_TimeToNextAttack -= Time.deltaTime;

            if (m_EnemyAttacker != null && m_EnemyAttacker.ReadyForAttack() && 
                RaycastUtils.IsVisible(m_EnemyAttacker.m_FireOrigin, m_EnemyAttacker.transform.right * -1, m_Player, m_EnemyAttacker.m_AngleDetected, m_EnemyAttacker.m_DistanceToAttack, GameConstants.PLAYER, new List<string> { GameConstants.ENEMY, GameConstants.SPELL, GameConstants.SPELL_IGNORE }) && m_TimeToNextAttack <= 0)
            {
                m_EnemyAttacker.AttackDone();
                m_Animator.SetTrigger(GameConstants.ATTACK);
                m_Changing = true;
            }
            else if (m_EnemyFollower != null &&
                !RaycastUtils.IsVisible(m_EnemyFollower.m_RayOrigin, m_EnemyFollower.transform.right * -1, m_Player, m_EnemyFollower.m_AngleDetected, m_EnemyFollower.m_DistanceToFollow, GameConstants.PLAYER, new List<string> { GameConstants.ENEMY, GameConstants.SPELL, GameConstants.SPELL_IGNORE }))
            {
                m_Animator.SetBool(GameConstants.IS_FOLLOW, false);
                m_Changing = true;
            }
        }
    }
}
