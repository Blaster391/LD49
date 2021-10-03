using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskUI : MonoBehaviour, IFlaskUI
{
    [SerializeField] private Image m_fill;

    [SerializeField] private ChemicalData m_startingChemical;

    #region Unity
    private void Awake()
    {
        if (m_startingChemical != null)
        {
            m_fill.color = m_startingChemical.Color;
        }
    }
    #endregion

    #region IFlaskUI
    public void SetChemical(ChemicalData i_chemical)
    {
        m_fill.color = i_chemical.Color;
    }
    #endregion
}
