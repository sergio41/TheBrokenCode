using UnityEngine;
using UnityEngine.UI;

public class NulloHealthController : MonoBehaviour
{
    public int m_Health;
    public Slider m_HealthBar;
    public Animator m_Animator;
    public AudioClip m_Die;

    int m_CurrentHealth;
    AudioSource m_AudioSource;

    void Start()
    {
        m_CurrentHealth = m_Health;
        m_AudioSource = GetComponent<AudioSource>();
        InitHealthBar();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(GameConstants.SPELL))
        {
            var controller = collider.gameObject.GetComponent<SpellController>();
            Damage(controller.m_SpellDamage);
        }
    }

    public void Damage(int damageValue)
    {
        m_CurrentHealth -= damageValue;
        if (m_CurrentHealth <= 0) {
            m_CurrentHealth = 0;
            Die();
        }
        m_HealthBar.value = m_CurrentHealth;
    }

    private void Die() 
    {
        if(m_Animator != null)
            m_Animator.SetTrigger(GameConstants.DIE);
        tag = "Untagged";
        m_AudioSource.PlayOneShot(m_Die);
        m_HealthBar.gameObject.SetActive(false);
        m_HealthBar.transform.parent.gameObject.SetActive(false);
    }

    public void InitHealthBar()
    {
        m_HealthBar.maxValue = m_Health;
        m_HealthBar.value = m_Health;
    }
}
