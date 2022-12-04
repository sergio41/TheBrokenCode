using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBillboardController : MonoBehaviour
{
    public GameObject m_CanvasTutorial;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(GameConstants.PLAYER))
        {
            m_CanvasTutorial.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(GameConstants.PLAYER))
        {
            m_CanvasTutorial.SetActive(false);
        }
    }
}
