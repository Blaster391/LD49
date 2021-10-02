using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    private ChemicalData m_chemical;
    [SerializeField]
    private int m_target = 10;
    [SerializeField]
    private Valve m_valve = null;
    [SerializeField]
    private float m_minValveThreshold = 0.25f;
    [SerializeField]
    private float m_spawnTime = 1.0f;

    private float m_lastSpawnTime = 0.0f;
    private int m_spawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_valve.GetValveProp() > m_minValveThreshold && m_spawnCount < m_target)
        {
            m_lastSpawnTime += Time.deltaTime * m_valve.GetValveProp();

            if(m_lastSpawnTime > m_spawnTime)
            {
                Spawn();

            }
        }
    }

    void Spawn()
    {
        m_lastSpawnTime = 0.0f;

        GameObject chemical = Instantiate<GameObject>(m_prefab, transform.position, transform.rotation, transform);
        chemical.GetComponent<ChemicalState>().ChangeState(m_chemical);

        m_spawnCount++;
    }
}
