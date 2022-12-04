using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SectionEventController : MonoBehaviour
{
    public string m_EventName;

    protected void CheckEvent()
    {
        var eventsDone = Fixeria.Instance.eventsDone[SceneManager.GetActiveScene().name];
        if (!eventsDone.ContainsKey(m_EventName))
            eventsDone.Add(m_EventName, false);
        else if (eventsDone[m_EventName])
            EventAlreadyDone();
    }

    protected abstract void EventAlreadyDone();

    protected void SetEventDone() 
    {
        Fixeria.Instance.eventsDone[SceneManager.GetActiveScene().name][m_EventName] = true;
    }
}
