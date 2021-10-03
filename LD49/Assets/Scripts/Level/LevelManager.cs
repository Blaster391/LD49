using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private bool m_isLevel = true;


    [SerializeField]
    private string m_nextLevel;

    [SerializeField] private int m_levelNumber = 0;
    public int LevelNumber => m_levelNumber;

    [SerializeField]
    private float m_timeLimit = 61.0f;

    [SerializeField]
    private GameObject m_explosionPrefab;

    [SerializeField]
    private AudioClip m_levelCompleteClip;
    [SerializeField]
    private AudioClip m_levelLoseClip;

    private float m_timeLeft = 0.0f;

    private List<ChemicalStore> m_stores = new List<ChemicalStore>();
    private bool m_complete = false;
    private bool m_gameOver = false;
    private float m_gameOverTime = 0.0f;
    private float m_completeTime = 0.0f;
    private SoundManager m_sound = null;

    private void Start()
    {
        m_timeLeft = m_timeLimit;
        m_sound = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }

        if (!m_isLevel)
        {
            return;
        }

        if (!m_complete && !m_gameOver)
        {
            m_complete = m_stores.TrueForAll(x => x.IsComplete());

            if(m_complete)
            {
                m_sound.StopBGM();
                m_sound.PlaySound(m_levelCompleteClip, true);
            }
        }

        if (m_complete)
        {
            m_completeTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLevel();
            }
        }

        if (m_gameOver)
        {
            m_gameOverTime += Time.deltaTime;
            return;
        }

        if(!m_complete && !m_gameOver)
        {
            m_timeLeft -= Time.deltaTime;
            if(m_timeLeft < 0.0f)
            {
                TriggerGameOver(Vector3.zero);
            }
        }
    }

    public void RegisterStore(ChemicalStore _store)
    {
        m_stores.Add(_store);
    }

    public bool LevelIsComplete()
    {
        return m_complete;
    }

    public bool LevelIsFailed()
    {
        return m_gameOver;
    }

    public float TimeLeft()
    {
        return Mathf.Max(0.0f, m_timeLeft);
    }

    public float GameOverTime()
    {
        return m_gameOverTime;
    }

    public float LevelCompleteTime()
    {
        return m_completeTime;
    }

    public void TriggerGameOver(Vector3 _explosionEffect)
    {
        if(!m_gameOver)
        {
            m_gameOver = true;
            Instantiate<GameObject>(m_explosionPrefab, _explosionEffect, Quaternion.identity, transform);


            m_sound.StopBGM();
            m_sound.PlaySound(m_levelLoseClip, true);
            
        }

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(m_nextLevel, LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void ToggleSound()
    {
        if (!PlayerPrefs.HasKey("Sound") || PlayerPrefs.GetInt("Sound") == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }


        PlayerPrefs.Save();
    }

    public void UpdateVolume(float i_volume)
    {
        PlayerPrefs.SetFloat("Volume", i_volume);
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {

#if UNITY_WEBGL
        SceneManager.LoadScene(0, LoadSceneMode.Single);
#else
        Application.Quit();
#endif
        
    }
}
