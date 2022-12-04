
using Assets.Scripts.Models;
using System.Linq;
using TMPro;
using UnityEngine;
using static GameEnums;

public class InventoryPanelController : MonoBehaviour
{
    void OnEnable() 
    {
        var inventoryItems = GetComponentsInChildren<InventoryItemController>(true);
        inventoryItems.ToList().ForEach(item => {
            item.gameObject.SetActive(Fixeria.Instance.inventory.Contains(item.m_Item));
        });
    }
}
