
using Assets.Scripts.Models;
using System.Linq;
using TMPro;
using UnityEngine;
using static GameEnums;

public class SpellPanelController : MonoBehaviour
{
    void OnEnable() 
    {
        var spellItems = GetComponentsInChildren<SpellItemController>(true);
        spellItems.ToList().ForEach(spell => {
            spell.gameObject.SetActive(Fixeria.Instance.learntSpells.ContainsKey(spell.m_Item));
            if (spell.gameObject.activeSelf && spell.m_LevelText != null)
                spell.m_LevelText.text = Fixeria.Instance.learntSpells[spell.m_Item] + "";
        });
    }
}
