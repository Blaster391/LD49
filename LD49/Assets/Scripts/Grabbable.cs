using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private bool m_adjustCentreOfMass = true;

    [SerializeField]
    private bool m_rotateOnGrab = false;

    private bool m_grabbed = false;
    private GameObject m_parent;
    private Rigidbody2D m_parentRigidbody;
    private Vector3 m_grabbedPosition = Vector3.zero;
    private Vector2 m_centreOfMass = Vector2.zero;
    private GrabManager m_manager = null;
    private float m_originalGravityScale = 0.0f;
    private float m_originalLinearDrag = 0.0f;
    private float m_originalAngularDrag = 0.0f;

    private bool m_travellingRight = false;
    private float m_previousZDirection = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_parent = transform.parent.gameObject;
        m_parentRigidbody = m_parent.GetComponent<Rigidbody2D>();

        m_centreOfMass = m_parentRigidbody.centerOfMass;
        m_originalGravityScale = m_parentRigidbody.gravityScale;
        m_originalLinearDrag = m_parentRigidbody.drag;
        m_originalAngularDrag = m_parentRigidbody.angularDrag;

        m_manager = GetComponentInParent<GrabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_grabbed)
        {

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 targetPoint = m_parent.transform.TransformPoint(m_grabbedPosition);
            Vector2 forceDirection = mouseWorld - targetPoint;

            if(!m_rotateOnGrab)
            {
                float rotationForce = 0.0f;
                if (Input.GetKey(KeyCode.Q))
                {
                    rotationForce = (1.0f * m_manager.RotateForce);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    rotationForce = (-1.0f * m_manager.RotateForce);
                }
                else
                {
                    rotationForce = (Mathf.Clamp(-Input.mouseScrollDelta.y * m_manager.ScrollWheelAdditive, -m_manager.ScrollWheelAdditive, m_manager.ScrollWheelAdditive) * m_manager.RotateForce);
                }

                rotationForce *= Time.deltaTime * 100.0f;

                m_parentRigidbody.AddTorque(rotationForce);
            }
            else
            {
                Vector2 lookPos = mouseWorld - targetPoint;
                float angle = Mathf.Atan2(lookPos.x, lookPos.y) * Mathf.Rad2Deg;

                if(angle < 0)
                {
                    angle += 360.0f;
                }

                var eulerAngles = m_parent.transform.rotation.eulerAngles;
                float zDirection = -eulerAngles.z;
                if (zDirection < 0)
                {
                    zDirection += 360.0f;
                }

                float delta = zDirection - angle;

                if (Mathf.Abs(delta) > 180.0f)
                {
                    delta = -delta;
                }

                delta /= 360.0f;



                delta = Mathf.Clamp(delta, -1.0f, 1.0f);

                Debug.Log($"{angle} - {zDirection} ({delta})");

                float rotationForce = m_manager.RotateForce * delta * Time.deltaTime * 100.0f;
                m_parentRigidbody.AddTorque(rotationForce);

                m_travellingRight = m_parentRigidbody.angularVelocity > 0.0f;
                m_previousZDirection = zDirection;

            }



            float distance = forceDirection.magnitude;
            forceDirection.Normalize();
            if (distance > m_manager.Threshold)
            {
                float force = Mathf.Lerp(m_manager.MinimumForce, m_manager.MaximumForce, distance / m_manager.MaximumForceDistance);
                force = Mathf.Clamp(force, m_manager.MinimumForce, m_manager.MaximumForce);
                force *= Time.deltaTime * 100.0f;
                m_parentRigidbody.AddForce(forceDirection * force);
            }
            else
            {
                m_parentRigidbody.velocity = m_parentRigidbody.velocity * 0.8f;
            }

        }
        
    }

    public void StartGrab()
    {
        Cursor.visible = true;

        Debug.Log(m_parent.name + " Grabbed");

        m_grabbed = true;
        m_parentRigidbody.gravityScale = 0.0f;
        m_parentRigidbody.drag = m_manager.LinearDrag;
        m_parentRigidbody.angularDrag = m_manager.AngularDrag;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        m_grabbedPosition = m_parent.transform.InverseTransformPoint(mouseWorld);

        if (m_adjustCentreOfMass)
        {
            m_parentRigidbody.centerOfMass = m_grabbedPosition;
        }

        Debug.Log(m_grabbedPosition);
    }

    public void EndGrab()
    {
        Cursor.visible = true;

        Debug.Log(m_parent.name + " Dropped");

        m_grabbed = false;
        m_parentRigidbody.gravityScale = m_originalGravityScale;

        m_parentRigidbody.angularDrag = m_originalAngularDrag;
        m_parentRigidbody.drag = m_originalLinearDrag;

        if(m_adjustCentreOfMass)
        {
            m_parentRigidbody.centerOfMass = m_centreOfMass;
        }

    }
}
