
using Assets.Scripts.Models;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameEnums;

public class SpellItemController : MonoBehaviour, IPointerEnterHandler
{
    public SpellEnum m_Item;
    public TextMeshProUGUI m_SpellNameText;
    public TextMeshProUGUI m_DescriptionPanelText;
    public TextMeshProUGUI m_LevelText;
    UISoundController m_Sound;

    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Sound.PlayHover();
        var text = GameConstants.spellDescriptions[m_Item];
        if (GameConstants.spellLevels.ContainsKey(m_Item))
            text = string.Format(text, GameConstants.spellLevels[m_Item][Fixeria.Instance.learntSpells[m_Item]].damage, GameConstants.spellLevels[m_Item][Fixeria.Instance.learntSpells[m_Item]].specialParameter);
        m_SpellNameText.text = GetSpellName();
        m_DescriptionPanelText.text = text;
    }

    private string GetSpellName() 
    {
        return m_Item.ToString().Substring(0, 1) + m_Item.ToString().Substring(1).ToLower();
    }
}
