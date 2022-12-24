using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameEnums;

public class FixeriaSpells : MonoBehaviour
{
    public float m_SpellCooldown;
    public Transform m_FirePosition;
    public List<Spell> m_Spells;
    public AudioClip m_Cast;

    float m_SpellCooldownCounter;
    Animator m_Animator;
    bool m_ReadyToFire;
    SpellEnum m_SpellToCast;
    AudioSource m_AudioSource;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
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
        m_AudioSource.PlayOneShot(m_Cast);
        m_Animator.SetTrigger(GameConstants.ATTACK);
        m_ReadyToFire = false;
        var spellToCast = m_Spells.Find(spell => spell.spell.Equals(m_SpellToCast)).spellObject;
        Instantiate(spellToCast, m_FirePosition.position, m_FirePosition.rotation, m_FirePosition.parent);
        m_SpellCooldownCounter = m_SpellCooldown;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Fixeria.Instance.castingStatus = GetActionStatus(context.action, false);
    }

    public void OnAttack2(InputAction.CallbackContext context)
    {
        Fixeria.Instance.castingStatus = GetActionStatus(context.action, true);
    }

    FixeriaSpellEnum GetActionStatus(InputAction inputAction, bool isLeft)
    {
        if (inputAction.triggered && inputAction.ReadValue<float>() > 0f 
            && !Fixeria.Instance.castingStatus.Equals(FixeriaSpellEnum.Casting)
            && m_SpellCooldownCounter <= 0) 
        {
            m_ReadyToFire = true;
            m_SpellToCast = isLeft ? Fixeria.Instance.activeSpellLeft : Fixeria.Instance.activeSpellRight;
            return FixeriaSpellEnum.Casting;
        }
        else if (inputAction.ReadValue<float>() == 0f) return FixeriaSpellEnum.Released;
        else return FixeriaSpellEnum.None;
    }
}
