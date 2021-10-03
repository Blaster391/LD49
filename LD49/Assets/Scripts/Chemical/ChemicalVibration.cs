using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChemicalVibration : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_vibrationAtTemperatureCurve;
    [SerializeField] private float m_vibrationForce;
    [SerializeField] private float m_vibrationForceMultiplierMin = 0.1f;
    [SerializeField] private float m_vibrationForceMultiplierMax = 1f;
    [SerializeField] private float m_vibrationMinTime = 0.1f;
    [SerializeField] private float m_vibrationMaxTime = 0.25f;

    private Rigidbody2D m_rigidbody = null;
    private ITemperature m_chemicalTempIF = null;

    private float m_vibrationDelay = 0f;

    #region Unity
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_chemicalTempIF = GetComponent<ITemperature>();
    }

    void Update()
    {
        if (m_vibrationDelay <= 0f)
        {
            float tempMult = m_vibrationAtTemperatureCurve.Evaluate(m_chemicalTempIF.Temperature);
            float forceMult = Random.Range(m_vibrationForceMultiplierMin, m_vibrationForceMultiplierMax);

            float force = m_vibrationForce * tempMult * forceMult * Time.deltaTime;

            float angle = Random.Range(0, Mathf.PI * 2f);

            Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));

            m_rigidbody.AddForce(direction * force);

            m_vibrationDelay = Random.Range(m_vibrationMinTime, m_vibrationMaxTime);
        }

        m_vibrationDelay -= Time.deltaTime;
    }
    #endregion
}
