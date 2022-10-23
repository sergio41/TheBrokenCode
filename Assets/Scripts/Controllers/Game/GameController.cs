using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image m_HealthFiller;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_HealthFiller.fillAmount = Fixeria.Instance.HealthPercentage();
    }
}
