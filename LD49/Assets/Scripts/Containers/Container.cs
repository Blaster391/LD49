using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public ITemperature Temperature { get; private set; } = null;
    public ContainerContents Contents { get; private set; } = null;

    #region Unity
    void Awake()
    {
        Temperature = GetComponent<ITemperature>();
        Contents = GetComponentInChildren<ContainerContents>();
    }
    #endregion
}
