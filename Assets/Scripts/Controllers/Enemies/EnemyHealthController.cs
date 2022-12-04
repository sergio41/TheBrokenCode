using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public int m_Health;
    public Slider m_HealthBar;

    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    int m_CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_CurrentHealth = m_Health;
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
        m_Animator.SetTrigger(GameConstants.DIE);
        m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
        tag = "Untagged";
    }

    public void InitHealthBar()
    {
        m_HealthBar.maxValue = m_Health;
        m_HealthBar.value = m_Health;
    }
}
