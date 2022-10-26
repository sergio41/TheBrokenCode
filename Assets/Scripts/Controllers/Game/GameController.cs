using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image m_HealthFiller;
    public GameObject m_PauseScreen;

    bool m_IsPaused;
    MapController m_MapController;


    // Start is called before the first frame update
    void Start()
    {
        m_MapController = FindObjectOfType<MapController>(true);
        m_MapController.m_CurrentSection = 1;
        Fixeria.Instance.m_VisitedSections.Add(SceneManager.GetActiveScene().name, new List<int> { m_MapController.m_CurrentSection });
    }

    // Update is called once per frame
    void Update()
    {
        m_HealthFiller.fillAmount = Fixeria.Instance.HealthPercentage();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        m_IsPaused = !m_IsPaused;
        m_PauseScreen.SetActive(m_IsPaused);
        Time.timeScale = m_IsPaused ? 0 : 1;
    }
}
