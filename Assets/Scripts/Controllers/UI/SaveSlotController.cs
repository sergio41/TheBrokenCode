using Assets.Scripts.Models;
using System;
using TMPro;
using UnityEngine;

public class SaveSlotController : MonoBehaviour
{
    public GameObject m_EmptySlot;
    public GameObject m_OccupiedSlot;
    public TextMeshProUGUI m_SectionText;
    public SpellForSlot[] m_Spells;
    public TextMeshProUGUI m_LastSaved;
    public TextMeshProUGUI m_Resources;
    public TextMeshProUGUI m_TimePlayed;

    void Start()
    {
        m_EmptySlot.SetActive(true);
        m_OccupiedSlot.SetActive(false);
    }

    public void LoadData(SaveGame data)
    {
        m_EmptySlot.SetActive(false);
        m_OccupiedSlot.SetActive(true);
        m_SectionText.text = string.Format(GameConstants.SAVE_SLOT_SECTION, data.sectionSaved);
        m_LastSaved.text = GetLastSavedDate(data.lastSave);
        m_Resources.text = data.resourceNumber + "";
        m_TimePlayed.text = GetTime(data.secondsPlayed) + "";
        foreach (SpellForSlot spell in m_Spells) 
        {
            spell.spellObject.SetActive(data.learntSpells.ContainsKey(spell.spell));
            if(GameConstants.spellLevels.ContainsKey(spell.spell))
                spell.levelText.text = data.learntSpells.ContainsKey(spell.spell) ? data.learntSpells[spell.spell] + "" : "";
        }
    }

    public void SetEmpty()
    {
        m_EmptySlot.SetActive(true);
        m_OccupiedSlot.SetActive(false);
    }

    public bool IsEmpty()
    {
        return m_EmptySlot.activeSelf;
    }

    private string GetLastSavedDate(long lastSave) 
    {        
        DateTime date = DateTime.FromBinary(lastSave);
        return date.ToString();
    } 

    private string GetTime(float time)
    {
        return time >= 0 ? TimeSpan.FromSeconds(time).ToString("hh\\:mm\\:ss") + "" : "--:--:---";
    }
}
