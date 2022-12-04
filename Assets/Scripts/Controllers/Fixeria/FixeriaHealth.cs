using Assets.Scripts.Models;
using UnityEngine;

public class FixeriaHealth : MonoBehaviour
{
    public float m_InvulnerabilityTime;
    public Transform m_FixeriaCenter;
    public AudioClip m_Hurt;
    public AudioClip m_Die;

    Rigidbody2D m_Rigidbody;
    Animator m_Animator;
    float m_CurrentInvulnerabilityTime;
    AudioSource m_AudioSource;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(m_CurrentInvulnerabilityTime > 0)
            m_CurrentInvulnerabilityTime -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.ENEMY))
            DamageByEnemy();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstants.ENEMY))
            DamageByEnemy();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(GameConstants.ENEMY))
            DamageByEnemy();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(GameConstants.ENEMY))
            DamageByEnemy();
    }

    void DamageByEnemy() 
    {
        if (m_CurrentInvulnerabilityTime <= 0)
        {
            Fixeria.Instance.currentHealth -= 1;
            m_CurrentInvulnerabilityTime = m_InvulnerabilityTime;
            if (Fixeria.Instance.currentHealth > 0)
            {
                m_Animator.SetTrigger(GameConstants.HURT);
                m_AudioSource.PlayOneShot(m_Hurt);
            }
            else
            {
                m_Animator.SetTrigger(GameConstants.DIE);
                m_AudioSource.PlayOneShot(m_Die);
                GetComponent<FixeriaMovement>().enabled = false;
                GetComponent<FixeriaSpells>().enabled = false;
                gameObject.layer = 31;
                m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
                enabled = false;
            }
        }
    }
}
