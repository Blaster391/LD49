using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalGravity : MonoBehaviour
{
    [SerializeField] private float m_liquidGravity = 1f;
    [SerializeField] private float m_gasGravity = -0.3f;

    private IChemicalState m_chemicalStateIF = null;
    private Rigidbody2D m_rigidbody = null;

    #region Unity
    void Awake()
    {
        m_chemicalStateIF = GetComponent<IChemicalState>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ApplyState(m_chemicalStateIF.State);

        m_chemicalStateIF.StateChanged += OnStateChange;
    }
    #endregion

    #region Listeners
    private void OnStateChange(ChemicalData i_from, ChemicalData i_to)
    {
        ApplyState(i_to);
    }
    #endregion

    #region Behavior
    private void ApplyState(ChemicalData i_state)
    {
        switch(i_state.MatterState)
        {
            case MatterState.Liquid:
                m_rigidbody.gravityScale = m_liquidGravity;
                break;

            case MatterState.Gas:
                m_rigidbody.gravityScale = m_gasGravity;
                break;
        }
    }
    #endregion
}
