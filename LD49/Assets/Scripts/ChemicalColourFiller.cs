using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalColourFiller : MonoBehaviour
{
    [SerializeField]
    private ChemicalData m_chemical;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = m_chemical.Color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
