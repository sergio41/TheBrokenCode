using UnityEngine;

public class UpgradeButtonController : MonoBehaviour
{
    [HideInInspector]
    public delegate void UpgradeFunction();
    [HideInInspector]
    public UpgradeFunction m_UpgradeFunction;

    UISoundController m_Sound;

    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
    }

    public void Upgrade() 
    {
        m_Sound.PlaySelect();
        m_UpgradeFunction();
    }
}
