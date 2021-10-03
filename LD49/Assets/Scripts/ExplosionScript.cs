using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField]
    private float m_fillScreenTime = 1.0f;

    [SerializeField]
    private float m_deleteTime = 3.0f;

    private LevelManager m_levelManager = null;
    void Start()
    {
        m_levelManager = GetComponentInParent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float destroyProp = m_levelManager.GameOverTime() / m_fillScreenTime;

        transform.localScale = new Vector3(1, 1, 1) * destroyProp * Screen.width * 1.1f;

        if (m_levelManager.GameOverTime() > m_deleteTime && !m_levelManager.LevelIsComplete())
        {
            Destroy(gameObject);
        }
    }
}
