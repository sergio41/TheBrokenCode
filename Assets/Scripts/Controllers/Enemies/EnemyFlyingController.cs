using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    public GameObject m_Enemy;
    public float m_ReturningSpeed;

    Animator m_Animator;
    bool m_IsReturning = false;

    void Start()
    {
        m_Animator = m_Enemy.GetComponentInParent<Animator>();    
    }

    private void Update()
    {
        if (m_IsReturning && m_Animator!= null && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Died"))
            Return();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(GameConstants.PLAYER) && m_Animator != null)
            m_Animator.SetBool(GameConstants.IS_FOLLOW, true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(GameConstants.PLAYER) && m_Animator != null)
        {
            m_Animator.SetBool(GameConstants.IS_FOLLOW, false);
            m_IsReturning = true;
            Return();
        }
    }

    private void Return()
    {
        var m_Direction = 1;
        if (transform.position.x < m_Enemy.transform.position.x)
            m_Direction = -1;

        if (m_Direction > 0)
            m_Enemy.transform.eulerAngles = new Vector2(0, 180);
        else if (m_Direction < 0)
            m_Enemy.transform.eulerAngles = new Vector2(0, 0);

        m_Enemy.transform.position = Vector2.MoveTowards(m_Enemy.transform.position, transform.position, m_ReturningSpeed * Time.deltaTime);
        if (Vector2.Distance(m_Enemy.transform.position, transform.position) <= 0.1)
            m_IsReturning = false;
    }
}
