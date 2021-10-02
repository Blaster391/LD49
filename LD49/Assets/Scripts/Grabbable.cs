using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;

public class Grabbable : MonoBehaviour
{

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    private bool m_grabbed = false;
    private GameObject m_parent;
    private Rigidbody2D m_parentRigidbody;
    private Vector3 m_clickPoint = Vector3.zero;
    private Vector3 m_grabbedPosition = Vector3.zero;
    private Vector2 m_centreOfMass = Vector2.zero;

    [SerializeField]
    private float m_movementForce = 10000.0f;

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

            if (forceDirection.magnitude > m_threshold)
            {
                forceDirection.Normalize();
                m_parentRigidbody.AddForce(forceDirection * m_movementForce * 0.1f);
            }

        }
        
    }

    public void StartGrab()
    {
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
        Debug.Log(m_parent.name + " Dropped");

        m_grabbed = false;
        m_parentRigidbody.gravityScale = 1.0f;

        m_parentRigidbody.angularDrag = 0.05f;
        m_parentRigidbody.drag = 0.0f;

        m_parentRigidbody.centerOfMass = m_centreOfMass;
    }
}
