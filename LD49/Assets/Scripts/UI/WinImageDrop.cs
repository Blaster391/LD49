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
    private RectTransform m_rectTransform = null;

    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();

        m_levelManager = GetComponentInParent<LevelManager>();

        m_startingY = m_rectTransform.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = m_rectTransform.anchoredPosition;
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



            pos.y += dropAmount;

        }

        m_rectTransform.anchoredPosition = pos;
    }
}
