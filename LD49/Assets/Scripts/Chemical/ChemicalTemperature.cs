using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalTemperature : TemperatureComponent
{
    private IChemicalState m_chemicalStateIF = null;

    #region Unity
    void Awake()
    {
        m_chemicalStateIF = GetComponent<IChemicalState>();
    }

    private void Start()
    {
        m_chemicalStateIF.StateChanged += OnStateChange;
    }

    protected override void Update()
    {
        base.Update();

        // See if we need to change state
        foreach (ChemicalData.TemperatureReaction tempReaction in m_chemicalStateIF.State.TemperatureReactions)
        {
            if (Temperature >= tempReaction.m_lowerBound && Temperature < tempReaction.m_upperBound)
            {
                if (tempReaction.m_result != null && tempReaction.m_result != m_chemicalStateIF.State)
                {
                    m_chemicalStateIF.ChangeState(tempReaction.m_result);


                    if(Temperature >= tempReaction.m_lowerBound)
                    {
                        GetComponent<ChemicalChangeSound>().HeatSound();
                    }
                    else
                    {
                        GetComponent<ChemicalChangeSound>().CoolSound();
                    }

                }
            }
        }
    }
    #endregion

    #region Listeners
    private void OnStateChange(ChemicalData i_from, ChemicalData i_to)
    {
        AlterTemperature(-Temperature);
    }
    #endregion
}
