using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private string m_nextLevel;

    private List<ChemicalStore> m_stores = new List<ChemicalStore>();
    private bool m_complete = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) // TODO maybe R bad?
        {
            RestartLevel();
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
    }

    public void RegisterStore(ChemicalStore _store)
    {
        m_stores.Add(_store);
    }

    public bool LevelIsComplete()
    {
        return m_complete;
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
