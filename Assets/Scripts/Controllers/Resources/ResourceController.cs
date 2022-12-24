using Assets.Scripts.Models;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public AudioClip m_Pick;

    UISoundController m_Sound;
    
    void Awake()
    {
        m_Sound = FindObjectOfType<UISoundController>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GameConstants.PLAYER))
        {
            Fixeria.Instance.resourceNumber++;
            m_Sound.PlayOther(m_Pick);
            Destroy(gameObject);
        }
    }
}
