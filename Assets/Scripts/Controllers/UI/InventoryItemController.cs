
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameEnums;

public class InventoryItemController : MonoBehaviour, IPointerEnterHandler
{
    public ItemEnum m_Item;
    public TextMeshProUGUI m_DescriptionPanelText;
    UISoundController m_Sound;

    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
    }
        
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Sound.PlayHover();
        m_DescriptionPanelText.text = GameConstants.descriptions[m_Item];
    }
}
