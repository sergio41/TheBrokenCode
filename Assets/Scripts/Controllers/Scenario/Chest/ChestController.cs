using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject m_ClosedChest;
    public GameObject m_OpenedChest;
    public GameObject m_DescriptionPanel;
    public TextMeshProUGUI m_DescriptionText;
    public float m_TimeToShowDescription;
    public AudioClip m_Open;

    Animator m_Animator;
    AudioSource m_AudioSource;
    protected bool m_Opened;
    float m_TimeToShowDescriptionLeft;

    protected void StartBase()
    {
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_Opened && m_TimeToShowDescriptionLeft > 0) 
        {
            m_TimeToShowDescriptionLeft -= Time.deltaTime;
            if (m_TimeToShowDescriptionLeft <= 0) 
            {
                m_DescriptionPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

    }

    protected bool OnOpenBase(Collision2D collision, string textToShow)
    {
        if (!m_Opened && collision.gameObject.CompareTag(GameConstants.PLAYER))
        {
            m_AudioSource.PlayOneShot(m_Open);
            m_Opened = true;
            m_Animator.SetTrigger(GameConstants.OPEN);
            m_TimeToShowDescriptionLeft = m_TimeToShowDescription;
            m_DescriptionText.text = textToShow + "\n\nPulsa Enter para cerrar.";
            return true;
        }
        return false;
    }

    protected void InitOpened() 
    {
        m_ClosedChest.SetActive(false);
        m_OpenedChest.SetActive(true);
        m_Opened = true;
    }
}
