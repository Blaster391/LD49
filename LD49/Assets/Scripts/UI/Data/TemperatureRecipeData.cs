using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TemperatureRecipeData", menuName = "ScriptableObjects/Recipes/Temperature", order = 1)]
public class TemperatureRecipeData : BaseRecipeData
{
    public enum Operations
    {
        Heat,
        Cool
    }

    [SerializeField] private ChemicalData m_chemical;
    [SerializeField] private Operations m_operation;
    [SerializeField] private ChemicalData m_chemicalResult;


    [SerializeField] private Recipe2Flasks m_uiPrefab;

    public override GameObject CreateRecipeUI(Transform i_parent)
    {
        Recipe2Flasks ui = Instantiate(m_uiPrefab, i_parent);
        ui.InitialiseRecipie(m_chemical, m_chemicalResult);
        return ui.gameObject;
    }

    public override ChemicalData[] Ingredients => new ChemicalData[] { m_chemical };
    public override ChemicalData Result => m_chemicalResult;
}
