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
    private Vector2 m_cachedMousePosition = Vector2.zero;

    [SerializeField]
    private float m_movementForce = 10000.0f;

    [SerializeField]
    private float m_rotateForce = 5000.0f;

    [SerializeField]
    private float m_angularDrag = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_parent = transform.parent.gameObject;
        m_parentRigidbody = m_parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_grabbed)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 forceDirection = mouseWorld - m_parent.transform.position;
            m_parentRigidbody.velocity = Vector3.zero;

            if (Input.GetMouseButtonDown(1))
            {
                m_cachedMousePosition = Input.mousePosition;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if(Input.GetMouseButtonUp(1))
            {
                SetCursorPos((int)m_cachedMousePosition.x, (int)m_cachedMousePosition.y);

                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetMouseButton(1))
            {
                m_parentRigidbody.AddTorque(Input.GetAxis("Rotate") * m_rotateForce);
            }
            else
            {
                m_parentRigidbody.AddTorque(Input.mouseScrollDelta.y * m_rotateForce);

                Cursor.lockState = CursorLockMode.None;

                forceDirection.Normalize();

                m_parentRigidbody.AddForce(forceDirection * m_movementForce);
            }
        }
    }

    public void StartGrab()
    {
        Debug.Log(m_parent.name + " Grabbed");

        m_grabbed = true;
        m_parentRigidbody.gravityScale = 0.0f;

        m_parentRigidbody.angularDrag = m_angularDrag;
    }

    public void EndGrab()
    {
        Cursor.lockState = CursorLockMode.None;

        Debug.Log(m_parent.name + " Dropped");

        m_grabbed = false;
        m_parentRigidbody.gravityScale = 1.0f;

        m_parentRigidbody.angularDrag = 0.0f;
    }
}
