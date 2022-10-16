using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameEnums;

public class FixeriaMovement : MonoBehaviour
{
    public float m_Speed = 12f;                 // How fast Fixeria moves
    public AudioSource m_MovementAudio;         // Reference to the audio source used to play the movement sounds of Fixeria

    private Fixeria m_Fixeria;                  // Object with the Fixeria Status
    private Rigidbody2D m_Rigidbody;            // Reference used to move Fixeria
    private Animator m_Animator;                // Reference control Fixeria's animations
    private float m_MovementInputValue;         // The current value of the movement input

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovementEffects();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (m_MovementInputValue < 0)
            transform.eulerAngles = new Vector2(0, 180);
        else if (m_MovementInputValue > 0)
            transform.eulerAngles = new Vector2(0, 0);

        m_Rigidbody.velocity = new Vector2(m_MovementInputValue * m_Speed, m_Rigidbody.velocity.y);
    }

    private void Jump()
    {
        m_Rigidbody.velocity = new Vector2(m_MovementInputValue * m_Speed, m_Rigidbody.velocity.y);
    }

    private void HandleMovementEffects()
    {              
        if (Mathf.Abs (m_MovementInputValue) < 0.1f)
        {
            if (m_Animator.GetBool("isRun"))
            {
                //m_MovementAudio.clip = null;
                m_Animator.SetBool("isRun", false);
                //m_MovementAudio.Play();
            }
        }
        else
        {
            if (!m_Animator.GetBool("isRun"))
            {
                //m_MovementAudio.clip = m_Steps;
                m_Animator.SetBool("isRun", true);
                //m_MovementAudio.Play();
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Animator.SetBool("isRun", true);
        m_MovementInputValue = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(m_Fixeria.jumpStatus.Equals(FixeriaJumpEnum.Grounded))
            m_Fixeria.jumpStatus = FixeriaJumpEnum.Jumping;
    }
}
