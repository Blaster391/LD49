using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerContents : MonoBehaviour
{
    private List<Chemical> m_containedParticles = new List<Chemical>();
    public List<Chemical> ContainedParticles => new List<Chemical>(m_containedParticles);

    private void OnTriggerEnter2D(Collider2D m_collider)
    {
        Chemical chemical = m_collider.gameObject.GetComponent<Chemical>();
        if (chemical != null)
        {
            m_containedParticles.Add(chemical);
            Debug.Log($"Chemical {m_collider.gameObject.name} entered {gameObject.name} container.");
        }
    }

    private void OnTriggerExit2D(Collider2D m_collider)
    {
        Chemical chemical = m_collider.gameObject.GetComponent<Chemical>();
        if (chemical != null)
        {
            m_containedParticles.Remove(chemical);
            Debug.Log($"Chemical {m_collider.gameObject.name} left {gameObject.name} container.");
        }
    }
}
