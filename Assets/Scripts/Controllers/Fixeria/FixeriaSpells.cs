using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameEnums;

public class FixeriaSpells : MonoBehaviour
{
    public float m_SpellCooldown;
    public Transform m_FirePosition;
    public GameObject m_Spell;

    float m_SpellCooldownCounter;
    Animator m_Animator;
    bool m_ReadyToFire;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleFire();
    }

    void HandleFire() 
    {
        if (m_SpellCooldownCounter > 0)
            m_SpellCooldownCounter -= Time.deltaTime;
        if (m_ReadyToFire && Fixeria.Instance.castingStatus.Equals(FixeriaSpellEnum.Casting))
        {
            Fire();
        }
    }

    void Fire() 
    {
        m_Animator.SetTrigger(GameConstants.ATTACK);
        m_ReadyToFire = false;
        Instantiate(m_Spell, m_FirePosition.position, m_FirePosition.rotation, m_FirePosition.parent);
        m_SpellCooldownCounter = m_SpellCooldown;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Fixeria.Instance.castingStatus = GetActionStatus(context.action);
    }


    FixeriaSpellEnum GetActionStatus(InputAction inputAction)
    {
        if (inputAction.triggered && inputAction.ReadValue<float>() > 0f 
            && !Fixeria.Instance.castingStatus.Equals(FixeriaSpellEnum.Casting)
            && m_SpellCooldownCounter <= 0) 
        {
            m_ReadyToFire = true; 
            return FixeriaSpellEnum.Casting;
        }
        //else if (inputAction.ReadValue<float>() > 0f) return FixeriaSpellEnum.Charging;
        else if (inputAction.ReadValue<float>() == 0f) return FixeriaSpellEnum.Released;
        else return FixeriaSpellEnum.None;
    }
}
