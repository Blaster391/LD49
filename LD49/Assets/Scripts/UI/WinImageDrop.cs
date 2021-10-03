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

    private float m_startingY = 0.0f;

    void Start()
    {
        m_startingY = transform.position.y;

        m_levelManager = GetComponentInParent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = m_startingY;

        if (m_levelManager.LevelIsComplete())
        {
            float time = m_levelManager.LevelCompleteTime();
            float dropAmount = 0.0f;

            if (time > m_dropTime)
            {
                dropAmount = m_dropAmount;
            }
            else if (time > m_dropDipTime)
            {
                float prop = (time - m_dropDipTime) / (m_dropTime - m_dropDipTime);
                dropAmount = Mathf.Lerp(m_dropAmountDip, m_dropAmount, prop);

            }
            else
            {
                float prop = (time ) / (m_dropDipTime);

                dropAmount = Mathf.Lerp(0.0f, m_dropAmountDip, prop);
            }

            pos.y += dropAmount * Screen.height;

        }

        transform.position = pos;
    }
}
