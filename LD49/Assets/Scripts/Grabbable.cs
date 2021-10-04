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

    [SerializeField]
    private float m_moveBoundUpper = 1.0f;
    [SerializeField]
    private float m_moveBoundLower = 1.0f;
    [SerializeField]
    private float m_moveSpeed = 1.0f;
    [SerializeField]
    private bool m_moveX = false;
    [SerializeField]
    private bool m_moveY = false;

    [SerializeField]
    private float m_forceMod = 1.0f;
    [SerializeField]
    private float m_rotateMod = 1.0f;

    private bool m_grabbed = false;
    private GameObject m_parent;
    private Rigidbody2D m_parentRigidbody;
    private Vector3 m_grabbedPosition = Vector3.zero;
    private Vector2 m_centreOfMass = Vector2.zero;
    private GrabManager m_manager = null;
    private float m_originalGravityScale = 0.0f;
    private float m_originalLinearDrag = 0.0f;
    private float m_originalAngularDrag = 0.0f;
    private Vector2 m_originalPosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_parent = transform.parent.gameObject;
        m_parentRigidbody = m_parent.GetComponent<Rigidbody2D>();

        m_centreOfMass = m_parentRigidbody.centerOfMass;
        m_originalGravityScale = m_parentRigidbody.gravityScale;
        m_originalLinearDrag = m_parentRigidbody.drag;
        m_originalAngularDrag = m_parentRigidbody.angularDrag;
        m_originalPosition = m_parent.transform.position;

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

            if(m_moveX || m_moveY)
            {
                Vector3 position = m_parent.transform.position;

                if(m_moveX)
                {
                    if(Mathf.Abs(targetPoint.x - mouseWorld.x) < 0.05f)
                    {
                        return;
                    }

                    if(targetPoint.x - mouseWorld.x > 0.0f)
                    {
                        position = position + Vector3.left * m_moveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        position = position - Vector3.left * m_moveSpeed * Time.deltaTime;
                    }
                }
                else
                {
                    if (Mathf.Abs(targetPoint.y - mouseWorld.y) < 0.05f)
                    {
                        return;
                    }

                    if (targetPoint.y - mouseWorld.y > 0.0f)
                    {
                        position = position + Vector3.down * m_moveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        position = position - Vector3.down * m_moveSpeed * Time.deltaTime;
                    }
                }

                float xDelta = position.x - m_originalPosition.x;
                float yDelta = position.y - m_originalPosition.y;

                if (xDelta > m_moveBoundUpper)
                {
                    position.x = m_originalPosition.x + m_moveBoundUpper;
                }

                if (xDelta < -m_moveBoundLower)
                {
                    position.x = m_originalPosition.x - m_moveBoundLower;
                }

                if (yDelta > m_moveBoundUpper)
                {
                    position.y = m_originalPosition.y + m_moveBoundUpper;
                }

                if (yDelta < -m_moveBoundLower)
                {
                    position.y = m_originalPosition.y - m_moveBoundLower;
                }


                m_parent.transform.position = position;
                return;
            }

            if(!m_rotateOnGrab)
            {
                float rotationForce = 0.0f;
                if (Input.GetKey(KeyCode.Q))
                {
                    rotationForce = (1.0f * m_manager.RotateForce * m_rotateMod);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    rotationForce = (-1.0f * m_manager.RotateForce * m_rotateMod);
                }
                else
                {
                    rotationForce = (Mathf.Clamp(-Input.mouseScrollDelta.y * Time.deltaTime * 60.0f * m_manager.ScrollWheelAdditive, -m_manager.ScrollWheelAdditive, m_manager.ScrollWheelAdditive) * m_manager.RotateForce * m_rotateMod);
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
                    delta = -delta * 0.1f;
                }

                delta /= 360.0f;



                delta = Mathf.Clamp(delta, -1.0f, 1.0f);

                float rotationForce = m_manager.RotateForce * m_rotateMod * delta * Time.deltaTime * 100.0f;
                m_parentRigidbody.AddTorque(rotationForce);

            }



            float distance = forceDirection.magnitude;
            forceDirection.Normalize();
            if (distance > m_manager.Threshold)
            {
                float force = Mathf.Lerp(m_manager.MinimumForce, m_manager.MaximumForce, distance / m_manager.MaximumForceDistance);
                force = Mathf.Clamp(force, m_manager.MinimumForce, m_manager.MaximumForce);
                force *= Time.deltaTime * 100.0f * m_forceMod;
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
