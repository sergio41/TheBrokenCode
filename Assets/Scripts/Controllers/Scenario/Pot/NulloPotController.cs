using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NulloPotController : MonoBehaviour
{
    public Transform m_NulloCenter;
    public Transform m_PotOrigin;

    LineRenderer m_LineRenderer;
    void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (m_NulloCenter != null)
        {
            m_LineRenderer.SetPosition(0, m_PotOrigin.position);
            m_LineRenderer.SetPosition(1, m_NulloCenter.position);
        }
        else {
            m_LineRenderer.enabled = false;
        }
    }
}