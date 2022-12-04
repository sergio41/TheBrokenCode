using Assets.Scripts.Models;
using UnityEngine;
using static GameEnums;

public class GoldenChestController : ChestController
{
    public SpellEnum m_Item;

    void Start()
    {
        StartBase();
        if (Fixeria.Instance.learntSpells.ContainsKey(m_Item))
            InitOpened();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var text = string.Format(GameConstants.spellDescriptions[m_Item], GameConstants.spellLevels[m_Item][1].damage, GameConstants.spellLevels[m_Item][1].specialParameter);
        if (OnOpenBase(collision, text))
            Fixeria.Instance.learntSpells.Add(m_Item, 1);
    }
}
