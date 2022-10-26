using UnityEngine;

public class SectionLoaderController : MonoBehaviour
{
    public Animator m_Animator;


    public void TriggerLoading() {
        m_Animator.SetTrigger(GameConstants.DO);
    }
}
