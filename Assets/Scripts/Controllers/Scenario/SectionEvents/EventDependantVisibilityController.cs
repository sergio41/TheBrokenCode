using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventDependantVisibilityController : MonoBehaviour
{
    public string m_EventName;
    
    void Start()
    {
        var eventsDone = Fixeria.Instance.eventsDone[SceneManager.GetActiveScene().name];
        if (!eventsDone.ContainsKey(m_EventName) || !eventsDone[m_EventName])
            gameObject.SetActive(false);
    }
}
