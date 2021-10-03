using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ContainerTrackingArea : MonoBehaviour
{
    [Header("Visible for runtime debugging")]
    [SerializeField] private List<Container> m_containers = new List<Container>();
    public List<Container> Containers => new List<Container>(m_containers);

    #region Unity
    private void OnTriggerEnter2D(Collider2D m_collider)
    {
        Container container = m_collider.gameObject.GetComponent<Container>();
        if (container != null)
        {
            m_containers.Add(container);
        }
    }

    private void OnTriggerExit2D(Collider2D m_collider)
    {
        Container container = m_collider.gameObject.GetComponent<Container>();
        if (container != null)
        {
            m_containers.Remove(container);
        }
    }
    #endregion
}
