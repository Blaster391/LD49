using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenValve : MonoBehaviour
{
    [SerializeField]
    private Bunsen m_bunsen = null;

    private Valve m_valve = null;

    private void Start()
    {
        m_valve = GetComponent<Valve>();
    }

    // Update is called once per frame
    void Update()
    {
        m_bunsen.Power = m_valve.GetValveProp();
    }
}
