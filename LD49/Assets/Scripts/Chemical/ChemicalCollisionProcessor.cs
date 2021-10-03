using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalCollisionProcessor : MonoBehaviour
{
    private IChemicalDatabase m_chemicalDatabaseIF = null;
    private IChemicalState m_chemicalStateIF = null;

    #region Unity
    private void Awake()
    {
        m_chemicalDatabaseIF = GetComponentInParent<IChemicalDatabase>();

        m_chemicalStateIF = GetComponent<IChemicalState>();
    }


    // ???
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IChemicalState otherStateIF = collision.collider.gameObject.GetComponent<IChemicalState>();

        if (otherStateIF != null)
        {
            // Chemical collision
            if (m_chemicalDatabaseIF.TryCollision(m_chemicalStateIF.State, otherStateIF.State, out ChemicalData chemicalResult))
            {
                m_chemicalStateIF.ChangeState(chemicalResult);
                otherStateIF.ChangeState(chemicalResult);

                GetComponent<ChemicalChangeSound>().CollisionSound();
            }
        }
    }
    #endregion
}
