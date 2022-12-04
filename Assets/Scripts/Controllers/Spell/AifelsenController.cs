using Assets.Scripts.Models;
using UnityEngine;

public class AifelsenController : SpellController
{
    [HideInInspector]
    public Quaternion m_Rotation = Quaternion.identity;
    [HideInInspector]
    public bool m_IsFirst = true;

    int m_NumberDivisions;

    // Start is called before the first frame update
    void Start()
    {
        m_SpellDamage = GameConstants.aifelsenLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.AIFELSEN]].damage;
        m_NumberDivisions = (int)GameConstants.aifelsenLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.AIFELSEN]].specialParameter;
        StartBase();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FixedUpdateBase();
    }

    protected override void SpellEffect(Collider2D collider)
    {
        if (IsColliderAfected(collider))
        {
            if (m_IsFirst)
            {
                var newSpellsPosition = transform.position + new Vector3(transform.rotation.eulerAngles.y == 180 ? -1.5f : 1.5f, 0f, 0f);
                var increment = 80 / (m_NumberDivisions - 1);
                for (int idx = 0; idx < m_NumberDivisions; idx ++) {
                    var spell = Instantiate(gameObject, newSpellsPosition, transform.rotation).GetComponent<AifelsenController>();
                    spell.m_Rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, - 40 + increment * idx));
                    spell.m_IsFirst = false;
                    spell.m_TimeToRelease = 0f;
                }
            }
            DestroySpell();
        }
    }

    protected override Quaternion SpellReleasedRotation()
    {
        return m_Rotation.Equals(Quaternion.identity) ? Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)) : m_Rotation;
    }
}
