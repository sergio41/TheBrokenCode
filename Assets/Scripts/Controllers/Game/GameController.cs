using Assets.Scripts.Models;
using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEnums;

public class GameController : MonoBehaviour
{
    public Image m_HealthFiller;
    public GameObject m_PauseScreen;
    public GameObject m_SaveScreen;
    public GameObject m_SaveConfirmationScreen;
    public Transform m_SectionPlaceholder;
    public TextMeshProUGUI m_SaveConfirmationText;
    public List<SpellImages> m_SpellImages;
    public Image m_LeftSpell;
    public Image m_RightSpell;
    public SaveSlotController[] m_Slots;
    public SectionController[] m_Sections;

    float m_TimePlayed = 0f;
    bool m_CanSave;
    bool m_IsPaused;
    MapController m_MapController;
    FixeriaHealth m_Fixeria;
    FixeriaMovement m_FixeriaMovement;
    int m_SlotToSave;
    FileDataHandler m_DataHandler;
    UISoundController m_Sound;

    void Awake()
    {
        m_MapController = FindObjectOfType<MapController>(true);
        m_Fixeria = FindObjectOfType<FixeriaHealth>(true);
        m_FixeriaMovement = FindObjectOfType<FixeriaMovement>(true);
        m_Sound = FindObjectOfType<UISoundController>();
        m_DataHandler = new FileDataHandler();
        var sectionToLoad = 1;
        var fixeriaPosition = new Vector3(m_Fixeria.transform.position.x, m_Fixeria.transform.position.y, m_Fixeria.transform.position.z);
        if (PlayerPrefs.GetInt(GameConstants.SLOT_TO_LOAD) >= 0)
        {
            var data = m_DataHandler.Load(PlayerPrefs.GetInt(GameConstants.SLOT_TO_LOAD) + "");
            Fixeria.LoadData(data);
            fixeriaPosition = data.position;
            m_TimePlayed = data.secondsPlayed;
            sectionToLoad = data.sectionSaved;
            m_LeftSpell.sprite = m_SpellImages.Find(image => image.spell.Equals(Fixeria.Instance.activeSpellLeft)).spellImage;
            m_RightSpell.sprite = m_SpellImages.Find(image => image.spell.Equals(Fixeria.Instance.activeSpellRight)).spellImage;
        }
        else
        {
            Fixeria.Reset();
            Fixeria.Instance.visitedSections.Add($"{SceneManager.GetActiveScene().name}#{sectionToLoad}");
            Fixeria.Instance.eventsDone.Add(SceneManager.GetActiveScene().name, new SerializableDictionary<string, bool>());
        }
        m_MapController.m_CurrentSection = sectionToLoad;
        var sectionPrefab = m_Sections.First(section => section.m_SectionNumber == sectionToLoad);
        var sectionLoaded = Instantiate(sectionPrefab, m_SectionPlaceholder);
        sectionLoaded.GetComponentInChildren<CinemachineVirtualCamera>().Follow = m_Fixeria.transform;
        m_Fixeria.transform.position = fixeriaPosition;
    }

    void Update()
    {
        m_HealthFiller.fillAmount = Fixeria.Instance.HealthPercentage();
        m_TimePlayed += Time.deltaTime;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!m_SaveScreen.activeSelf)
        {
            FindObjectsOfType<TutorialController>().ToList().ForEach(tutorial => { tutorial.gameObject.SetActive(false); Time.timeScale = 1; });
            m_IsPaused = !m_IsPaused;
            m_PauseScreen.SetActive(m_IsPaused);
            m_FixeriaMovement.MovementControl(!m_IsPaused, m_IsPaused);
            Time.timeScale = m_IsPaused ? 0 : 1;
        }
    }

    public void OnChangeLeftSpell(InputAction.CallbackContext context)
    {
        var value = context.action.ReadValue<float>();
        if (context.action.triggered && value != 0f && !m_PauseScreen.activeSelf)
        {
            
            Fixeria.Instance.activeSpellLeft = GetNextSpell(value, Fixeria.Instance.activeSpellLeft);
            m_LeftSpell.sprite = m_SpellImages.Find(image => image.spell.Equals(Fixeria.Instance.activeSpellLeft)).spellImage;
        }
    }

    public void OnChangeRightSpell(InputAction.CallbackContext context)
    {
        var value = context.action.ReadValue<float>();
        if (context.action.triggered && value != 0f && !m_PauseScreen.activeSelf)
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
    
    public void OnTrySave(InputAction.CallbackContext context)
    {
        if (m_CanSave && !m_PauseScreen.activeSelf) 
        {
            var data = m_DataHandler.LoadAllProfiles();
            for (int idx = 0; idx < m_Slots.Length; idx++) 
            {
                if (data.ContainsKey(idx + ""))
                    m_Slots[idx].LoadData(data[idx + ""]);
                else
                    m_Slots[idx].SetEmpty();
            }
            m_SaveScreen.SetActive(true);
            Fixeria.Instance.currentHealth = Fixeria.Instance.baseHealth;
            m_FixeriaMovement.MovementControl(false, true);
            Time.timeScale = 0;
        }
    }

    public void CloseSave()
    {
        m_Sound.PlaySelect();
        m_SaveScreen.SetActive(false);
        m_FixeriaMovement.MovementControl(true, false);
        Time.timeScale = 1;
    }

    public void ChooseSlotToSave(int slot)
    {
        m_Sound.PlaySelect();
        m_SlotToSave = slot;
        m_SaveConfirmationScreen.SetActive(true);
        m_SaveConfirmationText.text = string.Format(GameConstants.SAVE_SLOT_CONFIRMATION, m_SlotToSave + 1, m_Slots[slot].IsEmpty() ? "" : GameConstants.SAVE_SLOT_OVERRIDE);
    }

    public void SaveInSlot()
    {
        m_Sound.PlaySelect();
        var saveGame = new SaveGame(Fixeria.Instance);
        saveGame.scene = SceneManager.GetActiveScene().name;
        saveGame.lastSave = DateTime.Now.ToBinary();
        saveGame.secondsPlayed = m_TimePlayed;
        saveGame.sectionSaved = FindObjectOfType<SectionController>().m_SectionNumber;
        saveGame.position = m_Fixeria.transform.position;
        m_DataHandler.Save(saveGame, m_SlotToSave + "");
        m_Slots[m_SlotToSave].LoadData(saveGame);
        m_SaveConfirmationScreen.SetActive(false);
    }

    public void CloseSavePopUp()
    {
        m_Sound.PlaySelect();
        m_SaveConfirmationScreen.SetActive(false);
    }

    public void SetCanSave(bool canSave) { m_CanSave = canSave; }
}
