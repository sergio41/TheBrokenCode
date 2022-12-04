using Assets.Scripts.Models;
using UnityEngine;

public class InstacerController : SpellController
{
    void Start()
    {
        m_SpellDamage = 0;
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

    protected override Quaternion SpellReleasedRotation()
    {
        return Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
