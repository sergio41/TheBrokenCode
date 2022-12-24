using UnityEngine;

public class DiedState : StateMachineBehaviour
{
    public int m_ResourcesToCreate;
    public GameObject m_Resource;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
        for (int i = 0; i < m_ResourcesToCreate; i++)
        {
            Instantiate(m_Resource, animator.transform.position, animator.transform.rotation, animator.transform.parent.parent);
        }
    }
}
