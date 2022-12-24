using UnityEngine;

public class UISoundController : MonoBehaviour
{
    public AudioClip m_Hover;
    public AudioClip m_Select;
    public AudioClip m_Success;
    public AudioSource m_AudioSource;

    public void PlayHover() 
    {
        m_AudioSource.PlayOneShot(m_Hover);
    }

    public void PlaySelect()
    {
        m_AudioSource.PlayOneShot(m_Select);
    }

    public void PlaySuccess()
    {
        m_AudioSource.PlayOneShot(m_Success);
    }

    public void PlayOther(AudioClip audio)
    {
        m_AudioSource.PlayOneShot(audio);
    }
}
