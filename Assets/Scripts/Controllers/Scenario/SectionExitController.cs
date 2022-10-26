using Assets.Scripts.Models;
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionExitController : MonoBehaviour
{
    public GameObject m_CurrentSection;
    public GameObject m_SectionToLoad;
    public int m_PositionToLoadFixeria;

    SectionLoaderController m_LoaderController;
    MapController m_MapController;

    void Start()
    {
        m_LoaderController = FindObjectOfType<SectionLoaderController>();
        m_MapController = FindObjectOfType<MapController>(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GameConstants.PLAYER))
        {
            StartCoroutine(LoadSection());
        }
    }

    IEnumerator LoadSection() {
        m_LoaderController.TriggerLoading();
        var fixeriaMovement = FindObjectOfType<FixeriaMovement>();
        var fixeria = fixeriaMovement.gameObject;
        var placeholder = GetComponentInParent<SectionController>().transform.parent;
        fixeriaMovement.m_AbleToMove = false;

        yield return new WaitForSeconds(1.75f);

        Destroy(m_CurrentSection);

        var sectionLoaded = Instantiate(m_SectionToLoad, placeholder);
        var sectionController = sectionLoaded.GetComponent<SectionController>();
        var positionForFixeria = sectionLoaded.GetComponent<SectionController>().m_EntryPoints[m_PositionToLoadFixeria];
        fixeria.transform.position = positionForFixeria.position;
        fixeria.transform.rotation = positionForFixeria.rotation;
        sectionLoaded.GetComponentInChildren<CinemachineVirtualCamera>().Follow = fixeria.transform;
        m_MapController.m_CurrentSection = sectionController.m_SectionNumber;
        var visitedSection = Fixeria.Instance.m_VisitedSections;
        if (!visitedSection[SceneManager.GetActiveScene().name].Contains(m_MapController.m_CurrentSection))
            visitedSection[SceneManager.GetActiveScene().name].Add(m_MapController.m_CurrentSection);

        m_LoaderController.TriggerLoading();
        fixeriaMovement.m_AbleToMove = true;
    }
}
