using UnityEngine;

public class NulloAttack1State : StateMachineBehaviour
{
    public GameObject m_Fire;
    public float m_WaitUntilFire;
    public AudioClip m_CastSpell;

    float m_TimeLeftToAttack;
    Transform m_Player;
    NulloAttackController m_NulloAttacker;
    bool m_Attacking;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Player = FindObjectOfType<FixeriaHealth>().m_FixeriaCenter;
        m_NulloAttacker = animator.gameObject.GetComponent<NulloAttackController>();
        m_Attacking = false;
        m_TimeLeftToAttack = m_WaitUntilFire;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_TimeLeftToAttack -= Time.deltaTime;

        if (m_TimeLeftToAttack <= 0 && !m_Attacking)
        {
            if (m_NulloAttacker != null)
            {
                animator.gameObject.GetComponent<AudioSource>().PlayOneShot(m_CastSpell);
                m_Attacking = true;
                var angle = Mathf.Atan2(m_Player.position.y - m_NulloAttacker.m_SimpleAttackOrigin.position.y, m_Player.position.x - m_NulloAttacker.m_SimpleAttackOrigin.position.x) * Mathf.Rad2Deg;
                var rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                var fire = Instantiate(m_Fire, m_NulloAttacker.m_SimpleAttackOrigin.position, rotation).GetComponent<EnemyFireController>();
                fire.m_Rotation = rotation;
            }
        }
    }
}