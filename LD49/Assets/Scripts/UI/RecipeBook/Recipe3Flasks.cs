using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe3Flasks : MonoBehaviour
{
    [SerializeField] private FlaskUI m_flask1;
    [SerializeField] private FlaskUI m_flask2;
    [SerializeField] private FlaskUI m_flask3;

    public void InitialiseRecipie(ChemicalData i_chemical1, ChemicalData i_chemical2, ChemicalData i_chemical3)
    {
        m_flask1.SetChemical(i_chemical1);
        m_flask2.SetChemical(i_chemical2);
        m_flask3.SetChemical(i_chemical3);
    }
}
