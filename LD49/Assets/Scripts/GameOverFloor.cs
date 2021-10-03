using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFloor : MonoBehaviour
{
    private LevelManager m_manager = null;
    void Start()
    {
        m_manager = GetComponentInParent<LevelManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IChemicalState chemical = collision?.gameObject?.GetComponent<IChemicalState>();
        if(chemical != null)
        {
            m_manager.TriggerGameOver(collision.transform.position);

            Destroy(collision.gameObject);

        }
    }
}
