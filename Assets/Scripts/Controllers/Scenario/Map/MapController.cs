using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    [HideInInspector]
    public int m_CurrentSection;
    List<MapSectionController> m_MapSectionList;

    void OnEnable()
    {
        m_MapSectionList = new List<MapSectionController>(GetComponentsInChildren<MapSectionController>(true));
        m_MapSectionList.ForEach(section => section.gameObject.SetActive(Fixeria.Instance.m_VisitedSections[SceneManager.GetActiveScene().name].Contains(section.m_SectionNumber)));
        m_MapSectionList.ForEach(section => section.SetPlayerLocationVisibility(m_CurrentSection.Equals(section.m_SectionNumber)));
    }
}
