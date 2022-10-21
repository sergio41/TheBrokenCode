using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameEnums;

public class FixeriaMovement : MonoBehaviour
{
    public Transform m_FeetPosition;
    public LayerMask groundMask;
    public float m_Speed = 12f;
    public AudioSource m_MovementAudio;
    public float m_JumpTime = 2.0f;
    public float m_JumpForce = 2.0f;
    public float m_CheckRadius = 0.15f;

    Rigidbody2D m_Rigidbody;
    Animator m_Animator;
    float m_MovementInputValue;
    float m_JumpTimeCounter;

    private void Awake()
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
        var isGrounded = Physics2D.OverlapCircle(m_FeetPosition.position, m_CheckRadius, groundMask);
        switch (Fixeria.Instance.jumpStatus) {
            case FixeriaJumpEnum.Grounded:
                if (!isGrounded)
                    Fixeria.Instance.jumpStatus = FixeriaJumpEnum.Falling;
                m_Animator.SetBool("isJump", false);
                break;
            case FixeriaJumpEnum.Jumping:
                m_JumpTimeCounter -= Time.deltaTime;
                if (m_JumpTimeCounter > 0f)
                    Jump(m_JumpForce);
                else
                    Fixeria.Instance.jumpStatus = FixeriaJumpEnum.Falling;
                break;
            case FixeriaJumpEnum.Falling:
                if(isGrounded)
                    Fixeria.Instance.jumpStatus = FixeriaJumpEnum.Grounded;
                break;
        }
    }

    private void Jump(float appliedJumpForce)
    {
        m_Rigidbody.velocity = Vector2.up * appliedJumpForce;
        m_Animator.SetBool("isJump", true);
        m_Animator.SetBool("isRun", false);
    }

    private void HandleMovementEffects()
    {              
        if (Mathf.Abs (m_MovementInputValue) < 0.1f && m_Animator.GetBool("isRun"))
        {
            //m_MovementAudio.clip = null;
            m_Animator.SetBool("isRun", false);
            m_MovementAudio.Play();
        }
        else if (Mathf.Abs(m_MovementInputValue) >= 0.1f && !m_Animator.GetBool("isRun"))
        {
            //m_MovementAudio.clip = m_Steps;
            m_Animator.SetBool("isRun", true);
            m_MovementAudio.Play();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Animator.SetBool("isRun", true);
        m_MovementInputValue = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Fixeria.Instance.jumpStatus = GetJumpStatus(context.action);
    }

    private FixeriaJumpEnum GetJumpStatus(InputAction inputAction)
    {
        if (inputAction.triggered && inputAction.ReadValue<float>() > 0f && Fixeria.Instance.jumpStatus.Equals(FixeriaJumpEnum.Grounded))
        {
            //m_MovementAudio.clip = m_Steps;
            m_MovementAudio.Play();
            m_JumpTimeCounter = m_JumpTime;
            return FixeriaJumpEnum.Jumping;
        }
        else if (inputAction.ReadValue<float>() == 0f && Fixeria.Instance.jumpStatus.Equals(FixeriaJumpEnum.Jumping))
        {
            return FixeriaJumpEnum.Falling;
        }
        return Fixeria.Instance.jumpStatus;
    }
}
