using System.Collections.Generic;
using UnityEngine;

public abstract class SpellController : MonoBehaviour
{
    public float m_TimeToRelease = 0.4f;
    public float m_SpellForce = 20f;
    [HideInInspector]
    public int m_SpellDamage;
    public GameObject m_Explosion;

    bool released = false;
    float m_ReleaseTimeCounter;

    protected void StartBase()
    {
        m_ReleaseTimeCounter = m_TimeToRelease;
    }

    protected void FixedUpdateBase()
    {
        if(!released) Release();
    }

    void Release() 
    {
        m_ReleaseTimeCounter -= Time.deltaTime;
        if (m_ReleaseTimeCounter <= 0) {
            var rigidbody = gameObject.GetComponent<Rigidbody2D>();
            transform.rotation = SpellReleasedRotation();
            rigidbody.velocity = m_SpellForce * transform.right;
            rigidbody.constraints = RigidbodyConstraints2D.None;
            rigidbody.simulated = true;
            transform.parent = FindObjectOfType<SectionController>().transform;
            released = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        SpellEffect(collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SpellEffect(collision.collider);
    }

    protected abstract void SpellEffect(Collider2D collider);

    protected abstract Quaternion SpellReleasedRotation();

    protected void DestroySpell() 
    {
        Instantiate(m_Explosion, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }

    protected virtual bool IsColliderAfected(Collider2D collider) 
    {
        var listIgnoredTags = new List<string> { GameConstants.PLAYER, GameConstants.SPELL, GameConstants.SPELL_IGNORE };
        return !listIgnoredTags.Contains(collider.tag) && !31.Equals(collider.gameObject.layer);
    }
}
