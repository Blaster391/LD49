using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameFade : MonoBehaviour
{
    [SerializeField]
    private Color m_fromColor;
    [SerializeField]
    private Color m_toColor;

    [SerializeField]
    private float m_startTime = 0.0f;

    [SerializeField]
    private float m_time = 1.0f;

    private Image m_image = null;
    private LevelManager m_levelManager = null;

    void Start()
    {
        m_image = GetComponent<Image>();

        m_levelManager = GetComponentInParent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_levelManager.LevelIsComplete())
        {
            float time = m_levelManager.LevelCompleteTime();
            if(time > m_startTime)
            {
                float prop = (time - m_startTime) / (m_time - m_startTime);
                prop = Mathf.Clamp01(prop);

                m_image.color = Color.Lerp(m_fromColor, m_toColor, prop);

                Debug.Log(prop);
            }
            else
            {
                m_image.color = m_fromColor;
            }
        }
        else
        {
            m_image.color = m_fromColor;
        }
    }
}
