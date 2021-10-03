using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private float m_gameOverDelay = 3.0f;

    [SerializeField]
    private float m_screenFullDelay = 1.0f;

    [SerializeField]
    private GameObject m_gameOver;

    [SerializeField]
    private Image m_gameOverFade;

    [SerializeField]
    private Color m_initalColor;

    [SerializeField]
    private Color m_fadeColor;

    [SerializeField]
    private GameObject m_levelComplete;

    private LevelManager m_levelManager = null;

    // Start is called before the first frame update
    void Start()
    {
        m_gameOver.gameObject.SetActive(false);
        m_levelComplete.SetActive(false);
        m_gameOverFade.gameObject.SetActive(false);

        m_levelManager = GetComponentInParent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {


        if (m_levelManager.LevelIsComplete())
        {
            m_levelComplete.SetActive(true);
        }
        else if(m_levelManager.LevelIsFailed())
        {
            UpdateGameOverFade();

            if (m_screenFullDelay < m_levelManager.GameOverTime())
            {
                m_gameOver.SetActive(true);
            }
        }
        else
        {
            m_gameOver.gameObject.SetActive(false);
            m_levelComplete.SetActive(false);
            m_gameOverFade.gameObject.SetActive(false);

            //m_timer.gameObject.SetActive(true);

            //m_timer.text = $"{Mathf.RoundToInt(m_levelManager.TimeLeft())}";
        }
    }

    private void UpdateGameOverFade()
    {
        if(m_levelManager.GameOverTime() < m_gameOverDelay && m_levelManager.GameOverTime() > m_screenFullDelay)
        {
            m_gameOverFade.gameObject.SetActive(true);

            
            float adjustedTime = m_levelManager.GameOverTime() - m_screenFullDelay;
            float fadeProp = adjustedTime / (m_gameOverDelay - m_screenFullDelay);


            fadeProp = Mathf.Clamp01(fadeProp);

            Color fadeColor = Color.Lerp(m_initalColor, m_fadeColor, fadeProp);
            fadeColor.a = 1.0f - fadeProp;

            m_gameOverFade.color = fadeColor;

        }
        else
        {
            m_gameOverFade.gameObject.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        m_levelManager.RestartLevel();
    }

    public void QuitToMainMenu()
    {

    }

    public void NextLevel()
    {
        m_levelManager.NextLevel();
    }
}
