using System.Linq;
using TMPro;
using UnityEngine;

public class FirstLevelBossController : SectionEventController
{
    public GameObject m_Door;
    public GameObject m_Chest;
    public GameObject m_Portal;
    public GameObject m_Tutorial;
    public GameObject m_BossLive;
    public TextMeshProUGUI m_TutorialCounterBackText;
    public float m_TimeTutorialActive;
    public AudioClip m_BattleMusic;

    NulloAttackController m_BossAttack;
    bool m_EventActive;
    BoxCollider2D m_Collider;
    float m_TutorialTimeLeft;
    AudioClip m_OriginalBackMusic;
    AudioSource m_AudioSource;

    void Start()
    {
        m_BossAttack = transform.parent.gameObject.GetComponentInChildren<NulloAttackController>(true);
        m_Collider = GetComponent<BoxCollider2D>();
        m_AudioSource = FindObjectOfType<AudioListener>().gameObject.GetComponent<AudioSource>();
        m_OriginalBackMusic = m_AudioSource.clip;
        CheckEvent();
    }

    protected override void EventAlreadyDone()
    {
        m_Collider.enabled = false;
        m_Door.SetActive(false);
        m_Chest.SetActive(true);
        m_Portal.SetActive(true);
        Destroy(m_BossAttack.gameObject);
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

        if (m_EventActive && m_TutorialTimeLeft <= 0)
        {
            if (m_BossAttack == null)
            {
                m_EventActive = false;
                m_Door.SetActive(false);
                m_Chest.SetActive(true);
                m_Portal.SetActive(true);
                m_BossLive.SetActive(false);
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
            m_Door.SetActive(true);
            m_EventActive = true;
            m_Collider.enabled = false;
            m_TutorialTimeLeft = m_TimeTutorialActive;
            m_Tutorial.SetActive(true);
            Time.timeScale = 0;
            m_BossAttack.ReadyToAttack();
        }
    }
}
