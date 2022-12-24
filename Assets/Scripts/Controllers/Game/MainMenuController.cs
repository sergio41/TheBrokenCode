using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject m_Menu;
    public GameObject m_LoadGame;
    public GameObject m_Settings;
    public AudioMixer m_Mixer;
    public Slider m_AudioVolume;
    public SaveSlotController[] m_Slots;

    FileDataHandler m_DataHandler;
    UISoundController m_Sound;
    bool m_LoadData;
    Dictionary<string, SaveGame> m_SaveData;

    void Start()
    {
        m_DataHandler = new FileDataHandler();
        m_Sound = GetComponent<UISoundController>();
        m_Mixer.GetFloat("MusicVolume", out float actualVolume);
        m_AudioVolume.value = Mathf.Pow(10, actualVolume / 20);
        m_SaveData = m_DataHandler.LoadAllProfiles();
    }

    void Update()
    {
        if (m_LoadData)
        {
            for (int idx = 0; idx < m_Slots.Length; idx++)
            {
                if (m_SaveData.ContainsKey(idx + ""))
                    m_Slots[idx].LoadData(m_SaveData[idx + ""]);
                else
                    m_Slots[idx].SetEmpty();
            }
        }
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetInt(GameConstants.SLOT_TO_LOAD, -1);
        m_Sound.PlaySelect();
        SceneManager.LoadScene(GameConstants.CINEMATIC_SCENE);
    }

    public void OpenLoadGame()
    {
        m_Sound.PlaySelect();
        m_Menu.SetActive(false);
        m_LoadGame.SetActive(true);
        m_SaveData = m_DataHandler.LoadAllProfiles();
        m_LoadData = true;
    }

    public void CloseLoadGame()
    {
        m_Sound.PlaySelect();
        m_Menu.SetActive(true);
        m_LoadGame.SetActive(false);
        m_LoadData = false;
    }

    public void OpenSettings()
    {
        m_Sound.PlaySelect();
        m_Menu.SetActive(false);
        m_Settings.SetActive(true);
    }

    public void CloseSettings()
    {
        m_Sound.PlaySelect();
        m_Menu.SetActive(true);
        m_Settings.SetActive(false);
    }

    public void LoadGame(int slotToLoad)
    {
        m_Sound.PlaySelect();
        PlayerPrefs.SetInt(GameConstants.SLOT_TO_LOAD, slotToLoad);
        SceneManager.LoadScene(m_SaveData[slotToLoad + ""].scene);
    }

    public void OpenCredits()
    {
        m_Sound.PlaySelect();
        SceneManager.LoadScene(GameConstants.CREDITS_SCENE);
    }

    public void EndGame()
    {
        m_Sound.PlaySelect();
        Application.Quit();
    }

    public void AdjustVolume(float sliderValue)
    {
        m_Mixer.SetFloat(GameConstants.MUSIC_VOLUME, Mathf.Log10(sliderValue) * 20);
    }
}
