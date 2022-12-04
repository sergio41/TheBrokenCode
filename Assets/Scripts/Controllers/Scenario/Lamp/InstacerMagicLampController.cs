using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameEnums;

public class InstacerMagicLampController : MagicLampController
{
    public string m_EventName;
    public ItemEnum m_SchemeRequired;
    public GameObject m_ObjectToInstance;

    private void Start()
    {
        var eventsDone = Fixeria.Instance.eventsDone[SceneManager.GetActiveScene().name];
        if (!eventsDone.ContainsKey(m_EventName))
            eventsDone.Add(m_EventName, false);
        else if (eventsDone[m_EventName]) 
        {
            LightLamp();
            m_ObjectToInstance.SetActive(true);
        }
    }

    protected override void OnSpellImpact(Collider2D collider)
    {
        var isExpected = collider.gameObject.GetComponent<InstacerController>() != null && Fixeria.Instance.inventory.Contains(m_SchemeRequired);
        OnSpellImpactBase(collider, isExpected);
        if (isExpected)
        {
            m_ObjectToInstance.SetActive(true);
            Fixeria.Instance.eventsDone[SceneManager.GetActiveScene().name][m_EventName] = true;
        }
    }
}
