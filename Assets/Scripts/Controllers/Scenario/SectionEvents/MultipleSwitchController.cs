using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MultipleSwitchController : SectionEventController
{
    public GameObject m_Door;
    public float m_TimeToUnlight;

    List<MagicLampController> m_Lamps;
    float[] m_TimeToUnlightLeft;
    bool m_Activated;

    void Start()
    {
        m_Lamps = GetComponentsInChildren<MagicLampController>().ToList();
        m_TimeToUnlightLeft = new float[m_Lamps.Count];
        CheckEvent();
    }

    protected override void EventAlreadyDone()
    {
        m_Activated = true;
        m_Door.SetActive(false);
        m_Lamps.ForEach(lamp => lamp.LightLamp());
    }

    void Update()
    {
        if (!m_Activated)
        {
            if (!m_Lamps.Exists(lamp => !lamp.m_IsOn))
            {
                m_Activated = true;
                m_Door.SetActive(false);
                SetEventDone();
            }
            else
            {
                for (int idx = 0; idx < m_Lamps.Count; idx++)
                {
                    var lamp = m_Lamps[idx];
                    if (lamp.m_IsOn && m_TimeToUnlightLeft[idx] <= 0)
                        m_TimeToUnlightLeft[idx] = m_TimeToUnlight;
                    else if (lamp.m_IsOn)
                    {
                        m_TimeToUnlightLeft[idx] -= Time.deltaTime;
                        if (m_TimeToUnlightLeft[idx] <= 0)
                            lamp.UnLightLamp();
                    }

                }
            }
        }
    }
}
