using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChemicalStore : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro m_timerText = null;
    [SerializeField]
    private TextMeshPro m_countText = null;

    [SerializeField] 
    private ChemicalData m_targetChemicalType = null;
    [SerializeField]
    private uint m_targetChemicalCount = 0;

    private List<IChemicalState> m_containedChemicals = new List<IChemicalState>();
    private bool m_complete = false;
    private LevelManager m_levelManager = null;
    private uint m_lastCount = 0;

    private void Start()
    {
        m_levelManager = GetComponentInParent<LevelManager>();
        m_levelManager.RegisterStore(this);
    }


    void Update()
    {
        uint correctCount = 0;

        foreach(var chemical in m_containedChemicals)
        {
            if(chemical.State == m_targetChemicalType)
            {
                correctCount++;
            }
        }

        m_complete = correctCount >= m_targetChemicalCount;

        m_lastCount = correctCount;

        UpdateUI();
    }

    void UpdateUI()
    {
        m_timerText.text = $"{m_levelManager.TimeLeft():00.00}";

        m_countText.text = $"{m_lastCount}/{m_targetChemicalCount}";
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
