using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float m_TimeToDestroy;

    float m_TimeToDestroyLeft;

    // Start is called before the first frame update
    void Start()
    {
        m_TimeToDestroyLeft = m_TimeToDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeToDestroyLeft -= Time.deltaTime;
        if (m_TimeToDestroyLeft <= 0)
            Destroy(gameObject);
    }
}
