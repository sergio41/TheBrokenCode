using Assets.Scripts.Models;
using UnityEngine;

public class PrintioController : SpellController
{
    float m_BarVisibleTime;

    // Start is called before the first frame update
    void Start()
    {
        m_SpellDamage = GameConstants.aifelsenLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.PRINTIO]].damage;
        m_BarVisibleTime = GameConstants.aifelsenLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.PRINTIO]].specialParameter;
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
