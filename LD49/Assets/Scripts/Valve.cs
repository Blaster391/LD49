using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField]
    private float m_minRotation = 5.0f;

    [SerializeField]
    private float m_maxRotation = 355.0f;

    public float GetValveProp()
    {
        var eulerAngles = transform.rotation.eulerAngles;
        float zDirection = -eulerAngles.z;
        if (zDirection < 0)
        {
            zDirection += 360.0f;
        }

        zDirection = Mathf.Clamp(zDirection, m_minRotation, m_maxRotation);

        return (zDirection - m_minRotation) / m_maxRotation;
    }
}
