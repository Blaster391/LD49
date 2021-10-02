using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;

public class Grabbable : MonoBehaviour
{
    private bool m_grabbed = false;
    private GameObject m_parent;
    private Rigidbody2D m_parentRigidbody;
    private Vector3 m_grabbedPosition = Vector3.zero;
    private Vector2 m_centreOfMass = Vector2.zero;

    [SerializeField]
    private float m_rotateForce = 500.0f;

    [SerializeField]
    private float m_scrollWheelAdditive = 100.0f;

    [SerializeField]
    private float m_linearDrag = 1.0f;

    [SerializeField]
    private float m_angularDrag = 1.0f;

    [SerializeField]
    private float m_threshold = 0.1f;

    [SerializeField]
    private float m_minimumForce = 1000.0f;
    [SerializeField]
    private float m_maximumForce = 5000.0f;
    [SerializeField]
    private float m_maximumForceDistance = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_parent = transform.parent.gameObject;
        m_parentRigidbody = m_parent.GetComponent<Rigidbody2D>();

        m_centreOfMass = m_parentRigidbody.centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_grabbed)
        {

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 targetPoint = m_parent.transform.TransformPoint(m_grabbedPosition);
            Vector2 forceDirection = mouseWorld - targetPoint;

            float rotationForce = 0.0f;
            if (Input.GetKey(KeyCode.Q))
            {
                rotationForce = (1.0f * m_rotateForce);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rotationForce = (-1.0f * m_rotateForce);
            }
            else
            {
                rotationForce = (Input.mouseScrollDelta.y * m_rotateForce * m_scrollWheelAdditive);
            }

            m_parentRigidbody.AddTorque(rotationForce);

            float distance = forceDirection.magnitude;
            forceDirection.Normalize();
            if (distance > m_threshold)
            {
                float force = Mathf.Lerp(m_minimumForce, m_maximumForce, distance / m_maximumForceDistance);
                force = Mathf.Clamp(force, m_minimumForce, m_maximumForce);

                m_parentRigidbody.AddForce(forceDirection * force);
            }

        }
        
    }

    public void StartGrab()
    {
        Cursor.visible = true;

        Debug.Log(m_parent.name + " Grabbed");

        m_grabbed = true;
        m_parentRigidbody.gravityScale = 0.0f;
        m_parentRigidbody.drag = m_linearDrag;
        m_parentRigidbody.angularDrag = m_angularDrag;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        m_grabbedPosition = m_parent.transform.InverseTransformPoint(mouseWorld);

        m_parentRigidbody.centerOfMass = m_grabbedPosition;

        Debug.Log(m_grabbedPosition);
    }

    public void EndGrab()
    {
        Cursor.visible = true;

        Debug.Log(m_parent.name + " Dropped");

        m_grabbed = false;
        m_parentRigidbody.gravityScale = 1.0f;

        m_parentRigidbody.angularDrag = 0.05f;
        m_parentRigidbody.drag = 0.0f;

        m_parentRigidbody.centerOfMass = m_centreOfMass;
    }
}
