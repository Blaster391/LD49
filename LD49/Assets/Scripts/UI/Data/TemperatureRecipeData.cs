using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TemperatureRecipeData", menuName = "ScriptableObjects/Recipes/Temperature", order = 1)]
public class TemperatureRecipeData : BaseRecipeData
{
    [SerializeField] private ChemicalData m_chemicalA;
    [SerializeField] private ChemicalData m_chemicalB;

    public enum Operations
    {
        Heat, Cool
    }
    [SerializeField] private Operations m_operation;

    [SerializeField] private Recipe2Flasks m_uiPrefab;

    public override GameObject CreateRecipeUI(Transform i_parent)
    {
        Recipe2Flasks ui = Instantiate(m_uiPrefab, i_parent);
        ui.InitialiseRecipie(m_chemicalA, m_chemicalB);
        return ui.gameObject;
    }
}
