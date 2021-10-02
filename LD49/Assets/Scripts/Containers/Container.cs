using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public ContainerContents Contents { get; private set; } = null;

    void Awake()
    {
        Contents = GetComponentInChildren<ContainerContents>();
    }
}
