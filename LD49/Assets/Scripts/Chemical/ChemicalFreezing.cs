using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChemicalFreezing : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_dragMultAtTemperatureCurve;
    [SerializeField] private float m_dragCurveMultiplier = 1f;

    private Rigidbody2D m_rigidbody = null;
    private ITemperature m_chemicalTempIF = null;

    private float m_baseDrag = 1f;

    #region Unity
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_chemicalTempIF = GetComponent<ITemperature>();
    }

    private void Start()
    {
        m_baseDrag = m_rigidbody.drag;
    }

    void Update()
    {
        float dragCurveMult = m_dragMultAtTemperatureCurve.Evaluate(m_chemicalTempIF.Temperature);
        float dragChange = dragCurveMult - 1f;
        float dragMult = 1f + dragChange * m_dragCurveMultiplier;
        m_rigidbody.drag = m_baseDrag * dragMult;
    }
    #endregion
}
