using UnityEngine;
using UnityEngine.UI;

public class EnemySpellEffectController : MonoBehaviour
{
    public float m_BarVisibleTime;
    public Slider m_HealthBar;

    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    float m_BarVisibleTimeLeft;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_BarVisibleTimeLeft > 0)
            m_BarVisibleTimeLeft -= Time.deltaTime;
        else
            m_HealthBar.gameObject.SetActive(false);
    }

    public void ShowHealth()
    {
        m_BarVisibleTimeLeft = m_BarVisibleTime;
        m_HealthBar.gameObject.SetActive(true);
    }
}
