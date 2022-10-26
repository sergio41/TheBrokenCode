using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSectionController : MonoBehaviour
{
    
    public GameObject m_PlayerLocation;
    public int m_SectionNumber;

    public void SetPlayerLocationVisibility(bool isHere) 
    {
        m_PlayerLocation.SetActive(isHere);
    }
}
