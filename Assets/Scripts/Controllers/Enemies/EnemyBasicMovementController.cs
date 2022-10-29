using UnityEngine;
using UnityEngine.UI;
using static GameEnums;

public class EnemyBasicMovementController : MonoBehaviour
{
    public Transform m_Checker;
    public LayerMask m_GroundMask;
    public float m_CheckRadius;
    public float m_Speed;
    public float m_TimeWandering;
    public float m_TimeWaiting;

    Animator m_Animator;
    Rigidbody2D m_Rigidbody;
    EnemyHealthController m_HealthController;
    int m_Direction = -1;
    float m_TimeWaitingLeft;
    float m_TimeWanderingLeft;
    EnemyMovementEnum m_CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_HealthController = GetComponent<EnemyHealthController>();
        m_CurrentState = EnemyMovementEnum.Wandering;
        m_TimeWanderingLeft = m_TimeWandering;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_HealthController.m_HealthBar.value > 0)
            Move();
    }

    public void Move()
    {
        if (EnemyMovementEnum.Wandering.Equals(m_CurrentState))
        {
            m_TimeWanderingLeft -= Time.deltaTime;
            if (m_Direction > 0)
                transform.eulerAngles = new Vector2(0, 180);
            else if (m_Direction < 0)
                transform.eulerAngles = new Vector2(0, 0);

            m_Rigidbody.velocity = new Vector2(m_Direction * m_Speed, m_Rigidbody.velocity.y);
        }
        else if (EnemyMovementEnum.Waiting.Equals(m_CurrentState))
        {
            m_TimeWaitingLeft -= Time.deltaTime;
            m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
        }

        var isOutOfBounds = !Physics2D.OverlapCircle(m_Checker.position, m_CheckRadius, m_GroundMask);
        if ((isOutOfBounds || m_TimeWanderingLeft <= 0) && EnemyMovementEnum.Wandering.Equals(m_CurrentState)) 
        {
            m_CurrentState = EnemyMovementEnum.Waiting;
            m_TimeWaitingLeft = m_TimeWaiting;
        }
        else if(m_TimeWaitingLeft <= 0 && EnemyMovementEnum.Waiting.Equals(m_CurrentState))
        {
            m_CurrentState = EnemyMovementEnum.Wandering;
            m_TimeWanderingLeft = m_TimeWandering;
            m_Direction *= -1;
        }
        m_Animator.SetBool(GameConstants.IS_RUN, EnemyMovementEnum.Wandering.Equals(m_CurrentState));
    }
}
