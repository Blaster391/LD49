using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalTemperature : MonoBehaviour, IChemicalTemperature
{
    [SerializeField] private float m_temperature = 0f;
    [SerializeField] private float m_maxTemperature = 25f;
    [SerializeField] private float m_coolingPerSecond = 0.2f;

    private IChemicalState m_chemicalStateIF = null;

    #region Unity
    void Awake()
    {
        m_chemicalStateIF = GetComponent<IChemicalState>();
    }

    private void Update()
    {
        // Cool toward zero
        if (m_temperature > 0f)
        {
            AlterTemperature(-Mathf.Min(m_coolingPerSecond * Time.deltaTime, m_temperature));
        }
        else if (m_temperature < 0f)
        {
            AlterTemperature(Mathf.Min(m_coolingPerSecond * Time.deltaTime, m_temperature));
        }

        // See if we need to change state
        foreach (ChemicalData.TemperatureReaction tempReaction in m_chemicalStateIF.State.TemperatureReactions)
        {
            if (m_temperature >= tempReaction.m_lowerBound && m_temperature < tempReaction.m_upperBound)
            {
                if (tempReaction.m_result != null && tempReaction.m_result != m_chemicalStateIF.State)
                {
                    m_chemicalStateIF.ChangeState(tempReaction.m_result);
                }
            }
        }
    }
    #endregion

    #region IChemicalTexture
    public float Temperature => m_temperature;
    public float AlterTemperature(float i_change)
    {
        m_temperature = Mathf.Clamp(m_temperature + i_change, -m_maxTemperature, m_maxTemperature);
        return m_temperature;
    }
    #endregion
}
