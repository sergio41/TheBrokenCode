using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireController : SpellController
{
    [HideInInspector]
    public Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
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
            DestroySpell();
        }
    }

    protected override bool IsColliderAfected(Collider2D collider)
    {
        var listIgnoredTags = new List<string> { GameConstants.ENEMY, GameConstants.SPELL_IGNORE };
        return !listIgnoredTags.Contains(collider.tag) && !31.Equals(collider.gameObject.layer);
    }

    protected override Quaternion SpellReleasedRotation()
    {
        return m_Rotation;
    }
}
