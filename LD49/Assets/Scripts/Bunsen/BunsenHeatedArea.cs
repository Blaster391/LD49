using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BunsenHeatedArea : MonoBehaviour
{
    [Header("Visible for runtime debugging")]
    [SerializeField] private List<Container> m_heatedContainers = new List<Container>();
    public List<Container> HeatedContainers => new List<Container>(m_heatedContainers);

    #region Unity
    private void OnTriggerEnter2D(Collider2D m_collider)
    {
        Container container = m_collider.gameObject.GetComponent<Container>();
        if (container != null)
        {
            m_heatedContainers.Add(container);
        }
    }

    private void OnTriggerExit2D(Collider2D m_collider)
    {
        Container container = m_collider.gameObject.GetComponent<Container>();
        if (container != null)
        {
            m_heatedContainers.Remove(container);
        }
    }
    #endregion
}
