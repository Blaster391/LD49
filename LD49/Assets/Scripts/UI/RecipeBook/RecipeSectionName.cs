using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSectionName : MonoBehaviour
{
    [SerializeField] private Text m_name;

    public void InitialiseSection(ChemicalData i_chemical)
    {
        m_name.text = i_chemical.Name;
    }
}
