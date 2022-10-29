using UnityEngine;

public abstract class SpellController : MonoBehaviour
{
    public float m_TimeToRelease = 0.4f;
    public float m_SpellForce = 20f;
    public int m_SpellDamage = 20;

    bool released = false;
    float m_ReleaseTimeCounter;

    // Start is called before the first frame update
    protected void StartBase()
    {
        m_ReleaseTimeCounter = m_TimeToRelease;
    }

    // Update is called once per frame
    protected void FixedUpdateBase()
    {
        if(!released) Release();
    }

    void Release() 
    {
        m_ReleaseTimeCounter -= Time.deltaTime;
        if (m_ReleaseTimeCounter <= 0) {
            var rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = m_SpellForce * transform.right;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigidbody.simulated = true;
            transform.parent = null;
        }
    }
}
