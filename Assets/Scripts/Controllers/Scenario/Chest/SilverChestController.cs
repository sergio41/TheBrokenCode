using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnums;

public class SilverChestController : ChestController
{
    public ItemEnum m_Item;

    void Start()
    {
        StartBase();
        if (Fixeria.Instance.inventory.Contains(m_Item))
            InitOpened();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnOpenBase(collision, GameConstants.descriptions[m_Item]))
            Fixeria.Instance.inventory.Add(m_Item);
    }
}
