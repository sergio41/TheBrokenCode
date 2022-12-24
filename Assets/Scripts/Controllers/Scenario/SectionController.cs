using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
    public int m_SectionNumber;
    public List<Transform> m_EntryPoints;
    public float m_TimeToLetFixeriaMove = 0.8f;

    FixeriaMovement m_FixeriaMovement;
    SectionLoaderController m_LoaderController;
    bool m_IsLoading = false;
    float m_TimeLeftToLetFixeriaMove;

    void Start()
    {
        m_LoaderController = FindObjectOfType<SectionLoaderController>();
        m_FixeriaMovement = FindObjectOfType<FixeriaMovement>();
        m_FixeriaMovement.MovementControl(false, true);
        m_IsLoading = true;
        m_TimeLeftToLetFixeriaMove = m_TimeToLetFixeriaMove;
    }

    void Update()
    {
        m_TimeLeftToLetFixeriaMove -= Time.deltaTime;
        if (m_IsLoading && m_TimeLeftToLetFixeriaMove <= 0f) {
            m_FixeriaMovement.MovementControl(true, false);
            m_IsLoading = false;
        }        
    }
}
