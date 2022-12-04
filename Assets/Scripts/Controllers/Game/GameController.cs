using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEnums;

public class GameController : MonoBehaviour
{
    public Image m_HealthFiller;
    public GameObject m_PauseScreen;
    public List<SpellImages> m_SpellImages;
    public Image m_LeftSpell;
    public Image m_RightSpell;


    bool m_IsPaused;
    MapController m_MapController;

    void Awake()
    {
        m_MapController = FindObjectOfType<MapController>(true);
        m_MapController.m_CurrentSection = 1;
        Fixeria.Reset();
        Fixeria.Instance.visitedSections.Add(SceneManager.GetActiveScene().name, new List<int> { m_MapController.m_CurrentSection });
        Fixeria.Instance.eventsDone.Add(SceneManager.GetActiveScene().name, new Dictionary<string, bool>());
    }

    void Update()
    {
        m_HealthFiller.fillAmount = Fixeria.Instance.HealthPercentage();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        FindObjectsOfType<TutorialController>().ToList().ForEach(tutorial => { tutorial.gameObject.SetActive(false); Time.timeScale = 1; });
        m_IsPaused = !m_IsPaused;
        m_PauseScreen.SetActive(m_IsPaused);
        Time.timeScale = m_IsPaused ? 0 : 1;
    }

    public void OnChangeLeftSpell(InputAction.CallbackContext context)
    {
        var value = context.action.ReadValue<float>();
        if (context.action.triggered && value != 0f)
        {
            
            Fixeria.Instance.activeSpellLeft = GetNextSpell(value, Fixeria.Instance.activeSpellLeft);
            m_LeftSpell.sprite = m_SpellImages.Find(image => image.spell.Equals(Fixeria.Instance.activeSpellLeft)).spellImage;
        }
    }

    public void OnChangeRightSpell(InputAction.CallbackContext context)
    {
        var value = context.action.ReadValue<float>();
        if (context.action.triggered && value != 0f)
        {
            Fixeria.Instance.activeSpellRight = GetNextSpell(value, Fixeria.Instance.activeSpellRight);
            m_RightSpell.sprite = m_SpellImages.Find(image => image.spell.Equals(Fixeria.Instance.activeSpellRight)).spellImage;
        }
    }

    private SpellEnum GetNextSpell(float value, SpellEnum activeSpell) {
        var listOfSpells = Fixeria.Instance.learntSpells.Keys.ToList();
        var currentSpellIndex = listOfSpells.FindIndex(a => a.Equals(activeSpell));
        var nextSpellIndex = 0;
        if (value > 0f && currentSpellIndex < listOfSpells.Count - 1)
        {
            nextSpellIndex = currentSpellIndex + 1;
        }
        else if (value < 0f && currentSpellIndex > 0)
            nextSpellIndex = currentSpellIndex - 1;
        else if (value < 0f && currentSpellIndex == 0)
            nextSpellIndex = listOfSpells.Count - 1;
        return listOfSpells[nextSpellIndex];
    }

    public void OnCloseTutorials(InputAction.CallbackContext context)
    {
        FindObjectsOfType<TutorialController>().ToList().ForEach(tutorial => { tutorial.gameObject.SetActive(false); Time.timeScale = 1; });
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameConstants.MAIN_MENU_SCENE);
    }
}
