using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAdder : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_force = Vector2.up;

    List<Rigidbody2D> m_chemicals = new List<Rigidbody2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var chemical in m_chemicals)
        {
            Vector3 forceDirection = transform.TransformDirection(m_force);
            chemical.AddForce(forceDirection * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IChemicalState chemical = collision.GetComponent<IChemicalState>();
        if (chemical != null)
        {
            m_chemicals.Add(collision.attachedRigidbody);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        IChemicalState chemical = collision.GetComponent<IChemicalState>();
        if (chemical != null)
        {
            m_chemicals.Remove(collision.attachedRigidbody);
        }
    }
}
