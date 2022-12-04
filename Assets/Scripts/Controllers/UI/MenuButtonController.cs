
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string m_OriginalText;
    TextMeshProUGUI m_ButtonText;
    UISoundController m_Sound;

    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
        m_ButtonText = GetComponentInChildren<TextMeshProUGUI>(true);
        m_OriginalText = m_ButtonText.text;
    }

    void OnDisable()
    {
        m_ButtonText.text = m_OriginalText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_ButtonText.text = "<" + m_OriginalText + "/>";
        m_Sound.PlayHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_ButtonText.text = m_OriginalText;
    }
}
