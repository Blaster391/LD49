using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe2Flasks : MonoBehaviour
{
    [SerializeField] private FlaskUI m_flask1;
    [SerializeField] private FlaskUI m_flask2;

    public void InitialiseRecipie(ChemicalData i_chemical1, ChemicalData i_chemical2)
    {
        m_flask1.SetChemical(i_chemical1);
        m_flask2.SetChemical(i_chemical2);
    }
}
