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
        AlterTemperature(-m_coolingPerSecond * Time.deltaTime);

        // See if we need to change
        float currentBaseTemp = 0f;
        foreach (ChemicalData.TemperatureReaction tempReaction in m_chemicalStateIF.State.TemperatureReactions)
        {
            if (m_temperature >= currentBaseTemp && m_temperature < tempReaction.m_upperBound)
            {
                if (tempReaction.m_result != null && tempReaction.m_result != m_chemicalStateIF.State)
                {
                    m_chemicalStateIF.ChangeState(tempReaction.m_result);
                }
            }
            currentBaseTemp = tempReaction.m_upperBound;
        }
    }
    #endregion

    #region IChemicalTexture
    public float Temperature => m_temperature;
    public float AlterTemperature(float i_change)
    {
        m_temperature = Mathf.Clamp(m_temperature + i_change, 0f, m_maxTemperature);
        return m_temperature;
    }
    #endregion
}
