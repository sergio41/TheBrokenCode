using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    public GameObject m_Fire;
    public AudioClip m_AttackSound;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var m_Player = FindObjectOfType<FixeriaHealth>().m_FixeriaCenter;
        var m_EnemyAttacker = animator.gameObject.GetComponent<EnemyAttackerController>();
        animator.gameObject.GetComponent<AudioSource>().PlayOneShot(m_AttackSound);
        if (m_EnemyAttacker != null && m_Fire != null)
        {
            var angle = Mathf.Atan2(m_Player.position.y - m_EnemyAttacker.transform.position.y, m_Player.position.x - m_EnemyAttacker.transform.position.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            var fire = Instantiate(m_Fire, m_EnemyAttacker.m_FireOrigin.position, rotation).GetComponent<EnemyFireController>();
            fire.m_Rotation = rotation;
        }
    }
}
