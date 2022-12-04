using System.Linq;
using TMPro;
using UnityEngine;

public class KillEmAllController : SectionEventController
{
    public GameObject m_LeftDoor;
    public GameObject m_RightDoor;
    public GameObject m_EnemyCounter;
    public TextMeshProUGUI m_EnemyCounterText;
    public GameObject m_Tutorial;
    public TextMeshProUGUI m_TutorialCounterBackText;
    public float m_TimeTutorialActive;
    public AudioClip m_BattleMusic;

    EnemyHealthController[] m_EnemyList;
    bool m_EventActive;
    BoxCollider2D m_Collider;
    float m_TutorialTimeLeft;
    AudioClip m_OriginalBackMusic;
    AudioSource m_AudioSource;

    void Start()
    {
        m_EnemyList = transform.parent.gameObject.GetComponentsInChildren<EnemyHealthController>(true);
        m_Collider = GetComponent<BoxCollider2D>();
        m_AudioSource = FindObjectOfType<AudioListener>().gameObject.GetComponent<AudioSource>();
        m_OriginalBackMusic = m_AudioSource.clip;
        CheckEvent();
    }

    protected override void EventAlreadyDone()
    {
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

        if (m_EventActive && m_TutorialTimeLeft <= 0)
        {
            if (!m_EnemyList.ToList().Exists(enemy => enemy != null))
            {
                m_EventActive = false;
                m_LeftDoor.SetActive(false);
                m_RightDoor.SetActive(false);
                m_EnemyCounter.SetActive(false);
                m_AudioSource.clip = m_OriginalBackMusic;
                m_AudioSource.Play();
                SetEventDone();
            }
            else
            {
                m_EnemyCounterText.text = m_EnemyList.ToList().Count(enemy => enemy != null) + "";
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
            m_EnemyCounter.SetActive(true);
            m_EnemyCounterText.text = m_EnemyList.ToList().Count(enemy => enemy != null) + "";
            m_EventActive = true;
            m_Collider.enabled = false;
            m_TutorialTimeLeft = m_TimeTutorialActive;
            m_Tutorial.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
