using Cinemachine;
using UnityEngine;

public class SectionLoader : MonoBehaviour
{
    public GameObject m_CurrentSection;
    public GameObject m_SectionToLoad;
    public int m_PositionToLoadFixeria;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var fixeria = FindObjectOfType<FixeriaMovement>().gameObject;
            var placeholder = GetComponentInParent<SectionController>().transform.parent;

            Destroy(m_CurrentSection);

            var sectionLoaded = Instantiate(m_SectionToLoad, placeholder);
            var positionForFixeria = sectionLoaded.GetComponent<SectionController>().m_EntryPoints[m_PositionToLoadFixeria];
            fixeria.transform.position = positionForFixeria.position;
            fixeria.transform.rotation = positionForFixeria.rotation;
            sectionLoaded.GetComponentInChildren<CinemachineVirtualCamera>().Follow = fixeria.transform;
        }
    }
}
