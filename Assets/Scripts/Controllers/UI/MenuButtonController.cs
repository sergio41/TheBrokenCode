
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool m_HighlightWithMarks = true;

    string m_OriginalText;
    TextMeshProUGUI m_ButtonText;
    UISoundController m_Sound;

    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
        m_ButtonText = GetComponentInChildren<TextMeshProUGUI>(true);
        if(m_HighlightWithMarks)
            m_OriginalText = m_ButtonText.text;
    }

    void OnDisable()
    {
        if(m_HighlightWithMarks)
            m_ButtonText.text = m_OriginalText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(m_HighlightWithMarks)
            m_ButtonText.text = "<" + m_OriginalText + "/>";
        m_Sound.PlayHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(m_HighlightWithMarks)
            m_ButtonText.text = m_OriginalText;
    }
}
