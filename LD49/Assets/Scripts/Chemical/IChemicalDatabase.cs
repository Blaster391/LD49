using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChemicalDatabase
{
    ChemicalData GetChemical(string i_chemicalName);

    bool TryCollision(ChemicalData i_chemicalA, ChemicalData i_chemicalB, out ChemicalData o_chemicalResult);
}
