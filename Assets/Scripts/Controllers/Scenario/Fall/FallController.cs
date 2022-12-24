using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
    public Transform m_recovery;

    FixeriaHealth m_Fixeria; 
    
    void Start()
    {
        m_Fixeria = FindObjectOfType<FixeriaHealth>(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(GameConstants.PLAYER)) 
        {
            m_Fixeria.transform.position = m_recovery.position;
        }
    }
}
