using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  
 */
public class ChemicalState : MonoBehaviour, IChemicalState
{
    [SerializeField] private ChemicalData m_state = null;

    #region
    private void Awake()
    {
        if(m_state == null)
        {
            Debug.LogError("Starting without a starting Chemical state :(");
        }
    }
    #endregion

    #region IChemicalState
    public ChemicalData State => m_state;

    public void ChangeState(ChemicalData i_newState)
    {
        ChemicalData oldState = m_state;
        m_state = i_newState;
        StateChanged(oldState, m_state);
    }

    public Action<ChemicalData, ChemicalData> StateChanged { get; set; } = delegate { };
    #endregion
}
