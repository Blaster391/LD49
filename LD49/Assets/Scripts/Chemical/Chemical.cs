using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemical : MonoBehaviour
{
    public IChemicalState State { get; private set; } = null;
    public ITemperature Temperature { get; private set; } = null;

    #region Unity
    private void Awake()
    {
        State = GetComponent<IChemicalState>();
        Temperature = GetComponent<ITemperature>();
    }
    #endregion
}
