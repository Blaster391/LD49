using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls the colour of a chemical
 */
[RequireComponent(typeof(Rigidbody2D))]
public class ChemicalMass : ChemicalScalingPropertyBase<float>
{
    [SerializeField] private Rigidbody2D m_rigidbody = null;

    protected override float GetCurrentValue()
    {
        return m_rigidbody.mass;
    }

    protected override float GetTargetValue(ChemicalData i_data)
    {
        return i_data.Mass;
    }

    protected override float LerpValue(float i_startValue, float i_targetValue, float i_lerp)
    {
        return Mathf.Lerp(i_startValue, i_targetValue, i_lerp);
    }

    protected override void SetValue(float i_newValue)
    {
        m_rigidbody.mass = i_newValue;
    }
}
