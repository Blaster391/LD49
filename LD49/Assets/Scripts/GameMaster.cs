using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
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
