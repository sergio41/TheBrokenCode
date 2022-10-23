using Cinemachine;
using System.Collections;
using UnityEngine;

public class SectionExitController : MonoBehaviour
{
    public GameObject m_CurrentSection;
    public GameObject m_SectionToLoad;
    public int m_PositionToLoadFixeria;

    SectionLoaderController m_LoaderController;

    void Start()
    {
        m_LoaderController = FindObjectOfType<SectionLoaderController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
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
        var positionForFixeria = sectionLoaded.GetComponent<SectionController>().m_EntryPoints[m_PositionToLoadFixeria];
        fixeria.transform.position = positionForFixeria.position;
        fixeria.transform.rotation = positionForFixeria.rotation;
        sectionLoaded.GetComponentInChildren<CinemachineVirtualCamera>().Follow = fixeria.transform;

        m_LoaderController.TriggerLoading();
        fixeriaMovement.m_AbleToMove = true;
    }
}
