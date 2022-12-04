using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunController : SectionEventController
{
    public GameObject m_LeftDoor;
    public GameObject m_RightDoor;
    public GameObject m_OOM;
    public GameObject m_Tutorial;
    public TextMeshProUGUI m_TutorialCounterBackText;
    public float m_TimeTutorialActive;
    public AudioClip m_BattleMusic;

    List<MagicLampController> m_Lamps;
    float m_TutorialTimeLeft;
    bool m_EventActive;
    BoxCollider2D m_Collider;
    AudioClip m_OriginalBackMusic;
    AudioSource m_AudioSource;

    void Start()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        m_Lamps = gameObject.transform.parent.gameObject.GetComponentsInChildren<MagicLampController>().ToList();
        m_AudioSource = FindObjectOfType<AudioListener>().gameObject.GetComponent<AudioSource>();
        m_OriginalBackMusic = m_AudioSource.clip;
        CheckEvent();
    }

    protected override void EventAlreadyDone()
    {
        m_EventActive = false;
        m_LeftDoor.SetActive(false);
        m_RightDoor.SetActive(false);
        m_OOM.SetActive(false);
        m_Lamps.ForEach(lamp => lamp.LightLamp());
        m_Collider.enabled = false;
    }

    void Update()
    {
        if (m_TutorialTimeLeft > 0)
        {
            m_TutorialTimeLeft -= Time.unscaledDeltaTime;
            m_TutorialCounterBackText.text = (int)m_TutorialTimeLeft + "";
            if (m_TutorialTimeLeft <= 0)
            {
                Time.timeScale = 1;
                m_Tutorial.SetActive(false);
            }
        }

        if (m_EventActive)
        {
            if (!m_Lamps.Exists(lamp => !lamp.m_IsOn))
            {
                m_EventActive = false;
                m_LeftDoor.SetActive(false);
                m_RightDoor.SetActive(false);
                m_OOM.SetActive(false);
                m_AudioSource.clip = m_OriginalBackMusic;
                m_AudioSource.Play();
                SetEventDone();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(GameConstants.PLAYER))
        {
            m_AudioSource.clip = m_BattleMusic;
            m_AudioSource.Play();
            m_LeftDoor.SetActive(true);
            m_RightDoor.SetActive(true);
            m_OOM.SetActive(true);
            m_EventActive = true;
            m_Collider.enabled = false;
            m_TutorialTimeLeft = m_TimeTutorialActive;
            m_Tutorial.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
