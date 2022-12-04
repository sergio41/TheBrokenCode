using UnityEngine;

public class EnemySpellEffectController : MonoBehaviour
{
    public GameObject m_HealthBar;

    float m_BarVisibleTimeLeft;


    void Update()
    {
        if (m_BarVisibleTimeLeft > 0)
            m_BarVisibleTimeLeft -= Time.deltaTime;
        else
            m_HealthBar.gameObject.SetActive(false);
    }

    public void ShowHealth(float barVisibleTime)
    {
        m_BarVisibleTimeLeft = barVisibleTime;
        m_HealthBar.gameObject.SetActive(true);
    }
}
