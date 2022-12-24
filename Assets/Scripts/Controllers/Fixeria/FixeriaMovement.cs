using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static GameEnums;

public class FixeriaMovement : MonoBehaviour
{
    public Transform m_FeetPosition;
    public LayerMask m_GroundMask;
    public float m_Speed = 12f;
    public AudioClip m_Steps;
    public AudioClip m_Jump;
    public float m_JumpTime = 2.0f;
    public float m_JumpForce = 2.0f;
    public float m_CheckRadius = 0.15f;
        
    bool m_AbleToMove = true;
    Rigidbody2D m_Rigidbody;
    Animator m_Animator;
    float m_MovementInputValue;
    float m_JumpTimeCounter;
    AudioSource m_AudioSource;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (m_AbleToMove)
        {
            HandleMovementEffects();
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (!m_AbleToMove)
        {
            m_MovementInputValue = 0;
            m_Animator.SetBool(GameConstants.IS_RUN, false);
        }
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
        var isGrounded = Physics2D.OverlapCircle(m_FeetPosition.position, m_CheckRadius, m_GroundMask);
        switch (Fixeria.Instance.jumpStatus) {
            case FixeriaJumpEnum.Grounded:
                if (!isGrounded)
                    Fixeria.Instance.jumpStatus = FixeriaJumpEnum.Falling;
                m_Animator.SetBool(GameConstants.IS_JUMP, false);
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
        m_Animator.SetBool(GameConstants.IS_JUMP, true);
        m_Animator.SetBool(GameConstants.IS_RUN, false);
    }

    private void HandleMovementEffects()
    {              
        if (Mathf.Abs (m_MovementInputValue) < 0.1f && m_Animator.GetBool(GameConstants.IS_RUN))
        {
            m_Animator.SetBool(GameConstants.IS_RUN, false);
        }
        else if (Mathf.Abs(m_MovementInputValue) >= 0.1f && !m_Animator.GetBool(GameConstants.IS_RUN) && !m_Animator.GetBool(GameConstants.IS_JUMP))
        {
            m_Animator.SetBool(GameConstants.IS_RUN, true);
        }

        var isGrounded = Physics2D.OverlapCircle(m_FeetPosition.position, m_CheckRadius, m_GroundMask);
        if (m_Animator.GetBool(GameConstants.IS_RUN))
        {
            var alreadyPlayingSteps = m_Steps.Equals(m_AudioSource.clip);
            m_AudioSource.clip = isGrounded ? m_Steps : null;
            m_AudioSource.loop = isGrounded;
            if (!alreadyPlayingSteps)
                m_AudioSource.Play();
        }
        else {
            m_AudioSource.clip = null;
            m_AudioSource.loop = false;
            m_AudioSource.Play();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (m_AbleToMove)
        {
            m_Animator.SetBool(GameConstants.IS_RUN, true);
            m_MovementInputValue = context.ReadValue<float>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (m_AbleToMove)
            Fixeria.Instance.jumpStatus = GetJumpStatus(context.action);
    }

    private FixeriaJumpEnum GetJumpStatus(InputAction inputAction)
    {
        if (inputAction.triggered && inputAction.ReadValue<float>() > 0f && FixeriaJumpEnum.Grounded.Equals(Fixeria.Instance.jumpStatus))
        {
            m_AudioSource.PlayOneShot(m_Jump);
            m_JumpTimeCounter = m_JumpTime;
            return FixeriaJumpEnum.Jumping;
        }
        else if (inputAction.ReadValue<float>() == 0f && FixeriaJumpEnum.Jumping.Equals(Fixeria.Instance.jumpStatus))
        {
            return FixeriaJumpEnum.Falling;
        }
        return Fixeria.Instance.jumpStatus;
    }

    public void MovementControl(bool enabled, bool disableSound)
    {
        m_AbleToMove = enabled;
        if (enabled)
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        else
        {
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            Fixeria.Instance.jumpStatus = GameEnums.FixeriaJumpEnum.Falling;
            if (disableSound) 
            {
                m_AudioSource.clip = null;
                m_AudioSource.loop = false;
                m_AudioSource.Play();
            }
        }
    }
}
