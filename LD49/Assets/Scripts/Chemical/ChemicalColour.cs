using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls the colour of a chemical
 */
public class ChemicalColour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer = null;

    private IChemicalState m_chemicalStateIF = null;

    #region Unity
    void Awake()
    {
        m_chemicalStateIF = GetComponent<IChemicalState>();
    }

    void Start()
    {
        m_chemicalStateIF.StateChanged += OnStateChange;

        // Set initial colour
        Color color = m_chemicalStateIF.State.Color;
        m_spriteRenderer.color = color;
    }
    #endregion

    #region Listeners
    private void OnStateChange(ChemicalData i_from, ChemicalData i_to)
    {
        // Do instantly for now
        m_spriteRenderer.color = i_to.Color;
    }
    #endregion
}
