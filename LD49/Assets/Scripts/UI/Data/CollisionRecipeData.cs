using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollisionRecipeData", menuName = "ScriptableObjects/Recipes/Collision", order = 0)]
public class CollisionRecipeData : BaseRecipeData
{
    [SerializeField] private ChemicalCollisionReaction m_reaction;

    [SerializeField] private Recipe3Flasks m_uiPrefab;

    public override GameObject CreateRecipeUI(Transform i_parent)
    {
        Recipe3Flasks ui = Instantiate(m_uiPrefab, i_parent);
        ui.InitialiseRecipie(m_reaction.ChemicalA, m_reaction.ChemicalB, m_reaction.ChemicalResult);
        return ui.gameObject;
    }
}
