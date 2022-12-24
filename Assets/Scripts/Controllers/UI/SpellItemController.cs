
using Assets.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameEnums;

public class SpellItemController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public SpellEnum m_Item;
    public TextMeshProUGUI m_SpellNameText;
    public TextMeshProUGUI m_DescriptionPanelText;
    public TextMeshProUGUI m_LevelText;
    public TextMeshProUGUI m_UpgradeTitle;
    public TextMeshProUGUI m_UpgradeStats;
    public TextMeshProUGUI m_UpgradeCost;
    public GameObject m_UpgradeButton;
    public GameObject m_UpgradeDisabledButton;

    UISoundController m_Sound;

    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Sound.PlayHover();
        ShowSpellDescription();
    }

    private string GetSpellName() 
    {
        return m_Item.ToString().Substring(0, 1) + m_Item.ToString().Substring(1).ToLower();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_Sound.PlaySelect();
        ShowUpgradeInfo();
    }

    private void ShowSpellDescription() 
    {
        var text = GameConstants.spellDescriptions[m_Item][0] + GameConstants.spellDescriptions[m_Item][1];
        if (GameConstants.spellLevels.ContainsKey(m_Item))
            text = string.Format(text, GameConstants.spellLevels[m_Item][Fixeria.Instance.learntSpells[m_Item]].damage, GameConstants.spellLevels[m_Item][Fixeria.Instance.learntSpells[m_Item]].specialParameter);
        m_SpellNameText.text = GetSpellName();
        m_DescriptionPanelText.text = text;
    }

    private void ShowUpgradeInfo() 
    {
        string damage;
        string specialParameter;
        var title = "";
        var text = "";
        if (GameConstants.spellLevels.ContainsKey(m_Item))
        {
            var currentLevel = Fixeria.Instance.learntSpells[m_Item];
            title = GetSpellName();
            text = GameConstants.spellDescriptions[m_Item][1];
            if (GameConstants.spellLevels[m_Item].ContainsKey(currentLevel + 1))
            {
                damage = $"{GameConstants.spellLevels[m_Item][currentLevel].damage} -> <color=\"green\">{GameConstants.spellLevels[m_Item][currentLevel + 1].damage}</color>";
                specialParameter = $"{GameConstants.spellLevels[m_Item][currentLevel].specialParameter} -> <color=\"green\">{GameConstants.spellLevels[m_Item][currentLevel + 1].specialParameter}</color>";
                title += $" Nivel {currentLevel} -> <color=\"green\">{currentLevel + 1}</color>";
                m_UpgradeCost.text = GameConstants.spellLevels[m_Item][currentLevel + 1].cost + "";
                m_UpgradeCost.gameObject.SetActive(true);
                var enoughResources = Fixeria.Instance.resourceNumber >= GameConstants.spellLevels[m_Item][currentLevel + 1].cost;
                m_UpgradeButton.GetComponent<UpgradeButtonController>().m_UpgradeFunction = UpgradeSpell;
                m_UpgradeButton.SetActive(enoughResources);
                m_UpgradeDisabledButton.SetActive(!enoughResources);
            }
            else
            {
                title += " Nivel máximo";
                damage = $"{GameConstants.spellLevels[m_Item][currentLevel].damage}";
                specialParameter = $"{GameConstants.spellLevels[m_Item][currentLevel].specialParameter}";
                m_UpgradeButton.SetActive(false);
                m_UpgradeCost.gameObject.SetActive(false);
                m_UpgradeDisabledButton.SetActive(false);
            }
            text = string.Format(text, damage, specialParameter);
        }
        else
        {
            m_UpgradeButton.SetActive(false);
            m_UpgradeCost.gameObject.SetActive(false);
            m_UpgradeDisabledButton.SetActive(false);
        }
        m_UpgradeTitle.text = title;
        m_UpgradeStats.text = text;
    }

    private void UpgradeSpell() 
    {
        Fixeria.Instance.learntSpells[m_Item]++;
        Fixeria.Instance.resourceNumber -= GameConstants.spellLevels[m_Item][Fixeria.Instance.learntSpells[m_Item]].cost;
        if (m_SpellNameText.text.Equals(GetSpellName())) 
            ShowSpellDescription();
        m_LevelText.text = Fixeria.Instance.learntSpells[m_Item] + "";
        ShowUpgradeInfo();
    }
}
