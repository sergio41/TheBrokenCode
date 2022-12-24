using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Image m_MapButton;
    public Image m_InventoryButton;
    public Image m_SpellsButton;
    public Image m_SettingsButton;
    public GameObject m_MapTab;
    public GameObject m_InventoryTab;
    public GameObject m_SpellsTab;
    public GameObject m_SettingTab;
    public Color m_ActiveColor;
    public Color m_InactiveColor;
    public AudioMixer m_Mixer;
    public Slider m_AudioVolume;
    UISoundController m_Sound;

    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
    }

    void Start()
    {
        var map = FindObjectOfType<MapController>(true);
        map.transform.SetParent(m_MapTab.transform);
        map.gameObject.SetActive(true);
        m_Mixer.GetFloat(GameConstants.MUSIC_VOLUME, out float actualVolume);
        m_AudioVolume.value = Mathf.Pow(10, actualVolume / 20);
    }

    private void OnEnable()
    {
        OpenMap(false);
    }

    public void AdjustVolume(float sliderValue)
    {
        m_Mixer.SetFloat(GameConstants.MUSIC_VOLUME, Mathf.Log10(sliderValue) * 20);
    }

    public void OpenMap(bool doSound)
    {
        ActivateCorrectTabAndButton(m_MapTab, m_MapButton, doSound);
    }

    public void OpenInventory()
    {
        ActivateCorrectTabAndButton(m_InventoryTab, m_InventoryButton);
    }

    public void OpenSpells()
    {
        ActivateCorrectTabAndButton(m_SpellsTab, m_SpellsButton);
    }

    public void OpenSettings()
    {
        ActivateCorrectTabAndButton(m_SettingTab, m_SettingsButton);
    }

    private void ActivateCorrectTabAndButton(GameObject tabToActivate, Image buttonToActivate, bool doSound = true)
    {
        if(doSound)
            m_Sound.PlaySelect();
        var tabs = new List<GameObject>() { m_MapTab, m_InventoryTab, m_SpellsTab, m_SettingTab };
        var buttons = new List<Image>() { m_MapButton, m_InventoryButton, m_SpellsButton, m_SettingsButton };
        tabs.ForEach(tab => tab.SetActive(tab.Equals(tabToActivate)));
        buttons.ForEach(button => button.color = button.Equals(buttonToActivate) ? m_ActiveColor : m_InactiveColor);
    }
}
