using Assets.Scripts.Models;
using UnityEngine;

public class LoopforController : SpellController
{
    public float m_RotatingSpeed;
    public float m_Radius;
    public float m_TimeNextDamage;
    public GameObject m_Smoke;
    public GameObject m_FireDark;
    public GameObject m_Fire;
    public GameObject m_Sparks;

    bool m_Iterating;
    float m_TimeIteratingLeft;
    float m_Angle = 0;
    Vector2 m_Centre;
    CircleCollider2D m_Collider;
    float m_TimeTillNextDamage = 0;
    float m_TimeIterating; 

    void Start()
    {
        m_SpellDamage = GameConstants.loopforLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.LOOPFOR]].damage;
        m_TimeIterating = GameConstants.loopforLevels[Fixeria.Instance.learntSpells[GameEnums.SpellEnum.LOOPFOR]].specialParameter;
        m_Collider = GetComponent<CircleCollider2D>();
        StartBase();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Iterating)
            Iterate();
        else
            FixedUpdateBase();
    }

    protected override void SpellEffect(Collider2D collider)
    {
        if (IsColliderAfected(collider) && !m_Iterating)
        {
            m_TimeIteratingLeft = m_TimeIterating;
            m_Iterating = true;
            transform.parent = collider.transform;
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 90));
            m_Centre = new Vector2(transform.localPosition.x, transform.localPosition.y);
            m_Collider.enabled = false;
            m_TimeTillNextDamage = m_TimeNextDamage;
            m_Smoke.SetActive(false);
            m_FireDark.SetActive(false);
            m_Fire.SetActive(false);
            m_Sparks.SetActive(false);
        }
    }

    void Iterate()
    {
        m_TimeIteratingLeft -= Time.deltaTime;
        if (m_TimeIteratingLeft <= 0)
            DestroySpell();
        else 
        {
            m_TimeTillNextDamage -= Time.deltaTime;
            m_Collider.enabled = m_TimeTillNextDamage <= 0;
            if (m_TimeTillNextDamage <= 0)
                m_TimeTillNextDamage = m_TimeNextDamage;

            m_Angle += m_RotatingSpeed * Time.deltaTime;
            var offset = new Vector2(Mathf.Sin(m_Angle), Mathf.Cos(m_Angle)) * m_Radius;
            transform.localPosition = m_Centre + offset;
        }
    }

    protected override Quaternion SpellReleasedRotation()
    {
        return Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
