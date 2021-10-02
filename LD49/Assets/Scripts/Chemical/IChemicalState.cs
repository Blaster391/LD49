using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChemicalState
{
    ChemicalData State { get; }
    void ChangeState(ChemicalData i_newState);

    System.Action<ChemicalData, ChemicalData> StateChanged { get; set; }
}
