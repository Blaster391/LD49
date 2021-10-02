using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalDatabase : MonoBehaviour, IChemicalDatabase
{
    [SerializeField] private string m_chemicalsPath = "";
    [SerializeField] private string m_chemicalCollisionsPath = "";

    private Dictionary<string, ChemicalData> m_chemicals = new Dictionary<string, ChemicalData>();

    private Dictionary<ChemicalData, Dictionary<ChemicalData, ChemicalData>> m_chemicalCollisions = new Dictionary<ChemicalData, Dictionary<ChemicalData, ChemicalData>>();

    #region Unity
    private void Awake()
    {
        GatherChemicals();
        GatherChemicalCollisions();
    }
    #endregion

    #region IChemicalDatabase
    public ChemicalData GetChemical(string m_chemicalName)
    {
        bool exists = m_chemicals.TryGetValue(m_chemicalName, out ChemicalData chemicalData);
        if (!exists)
        {
            Debug.LogError($"Failed to find Chemical {m_chemicalName}");
        }
        return chemicalData;
    }

    public bool TryCollision(ChemicalData i_chemicalA, ChemicalData i_chemicalB, out ChemicalData o_chemicalResult)
    {
        if(m_chemicalCollisions.ContainsKey(i_chemicalA))
        {
            if(m_chemicalCollisions[i_chemicalA].ContainsKey(i_chemicalB))
            {
                o_chemicalResult = m_chemicalCollisions[i_chemicalA][i_chemicalB];
                return true;
            }
        }
        o_chemicalResult = null;
        return false;
    }
    #endregion

    #region Behavior
    private void GatherChemicals()
    {
        ChemicalData[] chemicals = Resources.LoadAll<ChemicalData>(m_chemicalsPath);
        foreach (ChemicalData chemicalData in chemicals)
        {
            m_chemicals.Add(chemicalData.Name, chemicalData);
        }
    }

    private void GatherChemicalCollisions()
    {
        ChemicalCollisionReaction[] chemicalCollisionReactions = Resources.LoadAll<ChemicalCollisionReaction>(m_chemicalCollisionsPath);
        foreach (ChemicalCollisionReaction chemicalCollisionReaction in chemicalCollisionReactions)
        {
            AddReaction(chemicalCollisionReaction.ChemicalA, chemicalCollisionReaction.ChemicalB, chemicalCollisionReaction.ChemicalResult);
            AddReaction(chemicalCollisionReaction.ChemicalB, chemicalCollisionReaction.ChemicalA, chemicalCollisionReaction.ChemicalResult);
        }
    }

    private Dictionary<ChemicalData, ChemicalData> EnsureReactionPrimary(ChemicalData i_chemical)
    {
        if (!m_chemicalCollisions.ContainsKey(i_chemical))
        {
            m_chemicalCollisions.Add(i_chemical, new Dictionary<ChemicalData, ChemicalData>());
        }
        return m_chemicalCollisions[i_chemical];
    }
    
    private void AddReaction(ChemicalData i_chemicalA, ChemicalData i_chemicalB, ChemicalData i_chemicalResult)
    {
        Dictionary<ChemicalData, ChemicalData> chemicalResults = EnsureReactionPrimary(i_chemicalA);
        if(chemicalResults.ContainsKey(i_chemicalB))
        {
            Debug.LogError($"Already a reaction recorded for {i_chemicalA.Name} and {i_chemicalB.Name}");
        }
        chemicalResults.Add(i_chemicalB, i_chemicalResult);
    }
    #endregion
}
