using UnityEngine;

public class EnemyAttackerController : MonoBehaviour
{
    public float m_DistanceToAttack;
    public float m_AttackCooldown;
    public Transform m_FireOrigin;
    public int m_AngleDetected;

    float m_TimeToNextAttack;

    void Update()
    {
        if (m_TimeToNextAttack > 0)
            m_TimeToNextAttack -= Time.deltaTime;
    }

    public void AttackDone() 
    {
        m_TimeToNextAttack = m_AttackCooldown;
    }

    public bool ReadyForAttack() 
    {
        return m_TimeToNextAttack <= 0;
    }
}
