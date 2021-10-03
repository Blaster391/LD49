using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureComponent : MonoBehaviour, ITemperature
{
    [SerializeField] private float m_temperature = 0f;
    [SerializeField] private float m_maxTemperature = 15f;
    [SerializeField] private float m_coolingPerSecond = 0.2f;

    #region Unity
    protected virtual void Update()
    {
        // Cool toward zero
        if (m_temperature > 0f)
        {
            AlterTemperature(-Mathf.Min(m_coolingPerSecond * Time.deltaTime, m_temperature));
        }
        else if (m_temperature < 0f)
        {
            AlterTemperature(Mathf.Max(m_coolingPerSecond * Time.deltaTime, m_temperature));
        }
    }
    #endregion

    #region ITemperature
    public float Temperature => m_temperature;
    public float HeatProp => Mathf.Max(0f, m_temperature / m_maxTemperature);
    public float CoolProp => Mathf.Max(0f, -m_temperature / m_maxTemperature);
    public float AlterTemperature(float i_change)
    {
        m_temperature = Mathf.Clamp(m_temperature + i_change, -m_maxTemperature, m_maxTemperature);
        return m_temperature;
    }
    #endregion
}
