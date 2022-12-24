using Assets.Scripts.Models;
using UnityEngine;

public class PrintioController : SpellController
{
    float m_BarVisibleTime;

    void Start()
    {
        m_SpellDamage = GameConstants.printioLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.PRINTIO]].damage;
        m_BarVisibleTime = GameConstants.printioLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.PRINTIO]].specialParameter;
        StartBase();
    }

    void FixedUpdate()
    {
        FixedUpdateBase();
    }

    protected override void SpellEffect(Collider2D collider)
    {
        if (IsColliderAfected(collider))
        {
            var controller = collider.gameObject.GetComponent<EnemySpellEffectController>();
            if(controller != null)
                controller.ShowHealth(m_BarVisibleTime);
            DestroySpell();
        }
    }

    protected override Quaternion SpellReleasedRotation()
    {
        return Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
