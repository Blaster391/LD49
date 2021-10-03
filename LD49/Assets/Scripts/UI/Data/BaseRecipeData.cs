using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseRecipeData : ScriptableObject, IComparable<BaseRecipeData>
{
    [SerializeField] private int m_unlockLevel = 1;

    public int UnlockLevel => m_unlockLevel;

    public abstract GameObject CreateRecipeUI(Transform i_parent);

    public abstract ChemicalData[] Ingredients { get; }
    public abstract ChemicalData Result { get; }

    public int CompareTo(BaseRecipeData other)
    {
        // Sort first by resulting chemical
        int nameCompareResult = string.Compare(Result.name, other.Result.name);
        if (nameCompareResult != 0)
        {
            return nameCompareResult;
        }

        // Then by the highest ingredient
        string highestIngredientAName = Ingredients.Max(x => x.name);
        string highestIngredientBName = other.Ingredients.Max(x => x.name);

        return string.Compare(highestIngredientAName, highestIngredientBName);
    }
}
