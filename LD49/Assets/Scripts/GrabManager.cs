using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{

    [SerializeField]
    private float m_rotateForce = 500.0f;
    public float RotateForce => m_rotateForce;

    [SerializeField]
    private float m_scrollWheelAdditive = 100.0f;
    public float ScrollWheelAdditive => m_scrollWheelAdditive;


    [SerializeField]
    private float m_linearDrag = 1.0f;
    public float LinearDrag => m_linearDrag;


    [SerializeField]
    private float m_angularDrag = 1.0f;
    public float AngularDrag => m_angularDrag;


    [SerializeField]
    private float m_threshold = 0.1f;
    public float Threshold => m_threshold;


    [SerializeField]
    private float m_minimumForce = 1000.0f;
    public float MinimumForce => m_minimumForce;

    [SerializeField]
    private float m_maximumForce = 5000.0f;
    public float MaximumForce => m_maximumForce;

    [SerializeField]
    private float m_maximumForceDistance = 100.0f;
    public float MaximumForceDistance => m_maximumForceDistance;

    // Start is called before the first frame update
    void Awake()
    {
        Physics2D.IgnoreLayerCollision(0, 8);
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    private Grabbable m_grabbable = null;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            int layer = 8;
            int layerMask = (1 << layer);
            layerMask = ~layerMask;

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D result = Physics2D.OverlapPoint(mouseWorld);

            m_grabbable = result?.transform?.gameObject?.GetComponent<Grabbable>();
            if(m_grabbable != null)
            {
                m_grabbable.StartGrab();
            }
        }

        if(Input.GetMouseButtonUp(0) && m_grabbable != null)
        {
            m_grabbable.EndGrab();
            m_grabbable = null;
        }

    }
}
