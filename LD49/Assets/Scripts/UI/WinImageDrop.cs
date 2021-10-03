using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinImageDrop : MonoBehaviour
{
    [SerializeField]
    private float m_dropAmount = 200.0f;

    [SerializeField]
    private float m_dropAmountDip = 210.0f;

    [SerializeField]
    private float m_dropTime = 3.0f;

    [SerializeField]
    private float m_dropDipTime = 2.0f;

    private LevelManager m_levelManager = null;

    void Start()
    {
        m_levelManager = GetComponentInParent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_levelManager.LevelIsComplete())
        {

        }
    }
}
