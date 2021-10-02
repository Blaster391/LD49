using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField]
    private bool m_invert = false;

    [SerializeField]
    private float m_minRotation = 5.0f;

    [SerializeField]
    private float m_maxRotation = 355.0f;

    [SerializeField]
    private bool m_startEnabled = false;

    public void Start()
    {
        var rigidbody = gameObject.GetComponent<Rigidbody2D>();
        float rotation = 0.0f;

        if(m_startEnabled || (!m_startEnabled && m_invert))
        {
            rotation = -m_maxRotation + 1;
        }
        else
        {
            rotation = -m_minRotation - 1;
        }


        rigidbody.rotation = rotation;
    }

    public float GetValveProp()
    {
        var eulerAngles = transform.rotation.eulerAngles;
        float zDirection = -eulerAngles.z;
        if (zDirection < 0)
        {
            zDirection += 360.0f;
        }

        zDirection = Mathf.Clamp(zDirection, m_minRotation, m_maxRotation);

        float result = (zDirection - m_minRotation) / m_maxRotation;
        if(m_invert)
        {
            result = 1.0f - result;
        }

        return result;
    }
}