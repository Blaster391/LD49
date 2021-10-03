using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    [SerializeField] private string m_recipePath;
    [SerializeField] private Transform m_recipeParent = null;

    private BaseRecipeData[] m_recipeData;

    private LevelManager m_levelManager = null;

    #region Unity
    private void Awake()
    {
        m_levelManager = GetComponentInParent<LevelManager>();
    }

    private void Start()
    {
        // Clear any visuals in the editor
        foreach (Transform child in m_recipeParent)
        {
            Destroy(child.gameObject);
        }

        m_recipeData = Resources.LoadAll<BaseRecipeData>(m_recipePath);
        ContructUIRecipes();
    }
    #endregion

    #region Behavior
    private void ContructUIRecipes()
    {
        List<BaseRecipeData> availableRecipes = new List<BaseRecipeData>(m_recipeData);

        if(m_levelManager != null)
        {
            // Cull based on current level
            int currentLevel = m_levelManager.LevelNumber;

            availableRecipes.RemoveAll(x => x.UnlockLevel > currentLevel);
        }

        foreach(BaseRecipeData recipe in availableRecipes)
        {
            GameObject recipeUI = recipe.CreateRecipeUI(m_recipeParent);
        }
    }
    #endregion
}
