using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRecipeData : ScriptableObject
{
    [SerializeField] private int m_unlockLevel = 1;
    [SerializeField] private int m_order = 0;

    public int UnlockLevel => m_unlockLevel;
    public int Order => m_order;

    public abstract GameObject CreateRecipeUI(Transform i_parent);
}
