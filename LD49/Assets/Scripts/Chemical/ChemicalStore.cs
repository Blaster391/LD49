using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalStore : MonoBehaviour
{
    [SerializeField] 
    private ChemicalData m_targetChemicalType = null;
    [SerializeField]
    private uint m_targetChemicalCount = 0;

    private List<IChemicalState> m_containedChemicals = new List<IChemicalState>();
    private bool m_complete = false;

    private void Start()
    {
       GetComponentInParent<LevelManager>().RegisterStore(this);
    }


    void Update()
    {
        uint correctCount = 0;

        if(m_containedChemicals.Count >= m_targetChemicalCount)
        {
            foreach(var chemical in m_containedChemicals)
            {
                if(chemical.State == m_targetChemicalType)
                {
                    correctCount++;
                }
            }
        }

        m_complete = correctCount >= m_targetChemicalCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IChemicalState chemical = collision.GetComponent<IChemicalState>();
        if(chemical != null)
        {
            m_containedChemicals.Add(chemical);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        IChemicalState chemical = collision.GetComponent<IChemicalState>();
        if (chemical != null)
        {
            m_containedChemicals.Remove(chemical);
        }
    }

    public bool IsComplete()
    {
        return m_complete;
    }

}
