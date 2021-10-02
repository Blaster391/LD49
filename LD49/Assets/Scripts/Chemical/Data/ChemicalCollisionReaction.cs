using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChemicalCollision", menuName = "ScriptableObjects/ChemicalCollision", order = 3)]
public class ChemicalCollisionReaction : ScriptableObject
{
    [SerializeField] private ChemicalData m_chemicalA;
    [SerializeField] private ChemicalData m_chemicalB;
    [SerializeField] private ChemicalData m_chemicalResult;

    public ChemicalData ChemicalA => m_chemicalA;
    public ChemicalData ChemicalB => m_chemicalB;
    public ChemicalData ChemicalResult => m_chemicalResult;
}
