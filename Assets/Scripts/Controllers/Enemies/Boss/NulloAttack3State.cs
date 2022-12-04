using UnityEngine;

public class NulloAttack3State : StateMachineBehaviour
{
    public GameObject m_Fire;
    public float[] m_WaitUntilFire;
    public AudioClip m_CastSpell;

    float m_TimeLeftToAttack;
    int m_FireAttemp;
    int m_FireOriginsToUse;
    Transform[][] m_FireOrigins;
    NulloAttackController m_NulloAttacker;
    bool m_Attacking;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_FireOriginsToUse = 0;
        m_FireAttemp = 0;
        m_NulloAttacker = animator.gameObject.GetComponent<NulloAttackController>();
        m_FireOrigins = new Transform[2][];
        m_FireOrigins[0] = m_NulloAttacker.m_ComplexAttackSetOne;
        m_FireOrigins[1] = m_NulloAttacker.m_ComplexAttackSetTwo;
        m_TimeLeftToAttack = m_WaitUntilFire[m_FireAttemp];
        animator.gameObject.GetComponent<AudioSource>().PlayOneShot(m_CastSpell);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_TimeLeftToAttack -= Time.deltaTime;
        if(m_TimeLeftToAttack > 0)
            m_Attacking = false;
        else if (m_TimeLeftToAttack <= 0 && !m_Attacking)
        {
            if (m_NulloAttacker != null)
            {
                m_Attacking = true;
                foreach (Transform origin in m_FireOrigins[m_FireOriginsToUse])
                {
                    var fire = Instantiate(m_Fire, origin.position, origin.rotation).GetComponent<EnemyFireController>();
                    fire.m_Rotation = origin.rotation;
                }
                m_FireOriginsToUse = Mathf.Abs(m_FireOriginsToUse - 1);
                m_FireAttemp++;
                if (m_FireAttemp < m_WaitUntilFire.Length)
                    m_TimeLeftToAttack = m_WaitUntilFire[m_FireAttemp];
            }
        }
    }
}
