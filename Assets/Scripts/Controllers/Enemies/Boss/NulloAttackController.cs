using UnityEngine;
using UnityEngine.UI;

public class NulloAttackController : MonoBehaviour
{
    public Transform m_SimpleAttackOrigin;
    public Transform[] m_ComplexAttackSetOne;
    public Transform[] m_ComplexAttackSetTwo;
    public bool m_DoSimpleAttack = false;
    public bool m_DoComplexAttack = false;

    Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void ReadyToAttack() 
    {
        m_Animator.SetTrigger(GameConstants.ATTACK);
    }
}
