using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject m_Menu;
    public GameObject m_Settings;
    public AudioMixer m_Mixer;
    public Slider m_AudioVolume;

    UISoundController m_Sound;

    void Start()
    {
        m_Sound = GetComponent<UISoundController>();
        m_Mixer.GetFloat("MusicVolume", out float actualVolume);
        m_AudioVolume.value = Mathf.Pow(10, actualVolume / 20);
    }

    public void StartNewGame()
    {
        m_Sound.PlaySelect();
        SceneManager.LoadScene(GameConstants.CINEMATIC_SCENE);
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
