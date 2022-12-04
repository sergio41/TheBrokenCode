using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicLampController : MonoBehaviour
{
    public GameObject m_LampOn;
    public GameObject m_LampOff;
    public GameObject m_LampGlow;
    [HideInInspector]
    public bool m_IsOn = false;


    void OnCollisionEnter2D(Collision2D collision)
    {
        OnSpellImpact(collision.collider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        OnSpellImpact(collider);
    }

    protected virtual void OnSpellImpact(Collider2D collider)
    {
        OnSpellImpactBase(collider, true);
    }

    protected void OnSpellImpactBase(Collider2D collider, bool extraCondition)
    {
        if (collider.CompareTag(GameConstants.SPELL) && extraCondition)
        {
            LightLamp();
        }
    }

    public void LightLamp()
    {
        m_LampOn.SetActive(true);
        m_LampOff.SetActive(false);
        m_LampGlow.SetActive(true);
        m_IsOn = true;
    }

    public void UnLightLamp()
    {
        m_LampOn.SetActive(false);
        m_LampOff.SetActive(true);
        m_LampGlow.SetActive(false);
        m_IsOn = false;
    }
}
