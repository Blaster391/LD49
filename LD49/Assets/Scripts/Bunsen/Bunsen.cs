using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Makes fire n tha
 */
public class Bunsen : MonoBehaviour, IBunsenControl
{
    [SerializeField] [Range(0, 1)] private float m_power = 0f;
    [SerializeField] [Range(1, 50)] private float m_temperatureChangePerSecond = 2f;

    private ContainerTrackingArea m_heatedArea = null;

    #region Unity
    private void Awake()
    {
        m_heatedArea = GetComponentInChildren<ContainerTrackingArea>();
    }

    private void Update()
    {
        if(m_power > 0f)
        {
            foreach(Container container in m_heatedArea.Containers)
            {
                container.Temperature.AlterTemperature(m_power * m_temperatureChangePerSecond * Time.deltaTime);
            }
        }
    }
    #endregion

    #region IBunsenControl
    public float Power
    {
        get => m_power;
        set => m_power = Mathf.Clamp01(value);
    }
    #endregion
}
