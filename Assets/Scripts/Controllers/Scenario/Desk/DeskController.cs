using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskController : MonoBehaviour
{
    public GameObject m_Tutorial;

    GameController m_GameController;

    void Start()
    {
        m_GameController = FindObjectOfType<GameController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(GameConstants.PLAYER)) 
        {
            m_Tutorial.SetActive(true);
            m_GameController.SetCanSave(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(GameConstants.PLAYER))
        {
            m_Tutorial.SetActive(false);
            m_GameController.SetCanSave(false);
        }
    }
}
