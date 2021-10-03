using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Makes cold n tha
 */
public class Fridge : MonoBehaviour
{
    [SerializeField] [Range(-10, 0)] private float m_temperatureChangePerSecond = -2f;

    private ContainerTrackingArea m_cooledArea = null;

    #region Unity
    private void Awake()
    {
        m_cooledArea = GetComponentInChildren<ContainerTrackingArea>();
    }

    private void Update()
    {
        foreach(Container container in m_cooledArea.Containers)
        {
            container.Temperature.AlterTemperature(m_temperatureChangePerSecond * Time.deltaTime);
        }
    }
    #endregion
}
