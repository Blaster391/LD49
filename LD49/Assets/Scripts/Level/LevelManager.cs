using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private string m_nextLevel;

    [SerializeField]
    private float m_timeLimit = 61.0f;

    private float m_timeLeft = 0.0f;

    private List<ChemicalStore> m_stores = new List<ChemicalStore>();
    private bool m_complete = false;
    private bool m_gameOver = false;

    private void Start()
    {
        m_timeLeft = m_timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) // TODO maybe R bad?
        {
            RestartLevel();
        }

        if(m_gameOver)
        {
            return;
        }


        if(!m_complete)
        {
            m_complete = m_stores.TrueForAll(x => x.IsComplete());
        }

        if(m_complete)
        {
            Debug.Log("u r winner");

            if(Input.GetKeyDown(KeyCode.Space))
            {
                NextLevel();
            }

        }
        else
        {
            m_timeLeft -= Time.deltaTime;
            m_gameOver = m_timeLeft < 0.0f;
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
        return m_timeLeft;
    }

    public void TriggerGameOver()
    {
        m_gameOver = true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(m_nextLevel, LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
