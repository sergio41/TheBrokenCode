using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static GameEnums;

public class FixeriaHealth : MonoBehaviour
{
    public float m_InvulnerabilityTime;

    Rigidbody2D m_Rigidbody;
    Animator m_Animator;
    float m_CurrentInvulnerabilityTime;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(m_CurrentInvulnerabilityTime > 0)
            m_CurrentInvulnerabilityTime -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(GameConstants.ENEMY))
            DamageByEnemy();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(GameConstants.ENEMY))
            DamageByEnemy();
    }

    void DamageByEnemy() 
    {
        if (m_CurrentInvulnerabilityTime <= 0)
        {
            Fixeria.Instance.m_CurrentHealth -= 1;
            m_CurrentInvulnerabilityTime = m_InvulnerabilityTime;
            if (Fixeria.Instance.m_CurrentHealth > 0)
                m_Animator.SetTrigger(GameConstants.HURT);
            else
            {
                m_Animator.SetTrigger(GameConstants.DIE);
                GetComponent<FixeriaMovement>().enabled = false;
                GetComponent<FixeriaSpells>().enabled = false;
                gameObject.layer = 31;
                m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                enabled = false;
            }
        }
    }
}
