using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_gameOver;

    [SerializeField]
    private TextMeshProUGUI m_levelComplete;

    private LevelManager m_levelManager = null;

    // Start is called before the first frame update
    void Start()
    {
        m_gameOver.gameObject.SetActive(false);
        m_levelComplete.gameObject.SetActive(false);

        m_levelManager = GetComponentInParent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_gameOver.gameObject.SetActive(false);
        m_levelComplete.gameObject.SetActive(false);

        if (m_levelManager.LevelIsComplete())
        {
            m_levelComplete.gameObject.SetActive(true);
        }
        else if(m_levelManager.LevelIsFailed())
        {
            m_gameOver.gameObject.SetActive(true);
        }
        else
        {
            //m_timer.gameObject.SetActive(true);

            //m_timer.text = $"{Mathf.RoundToInt(m_levelManager.TimeLeft())}";
        }
    }
}
