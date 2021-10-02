using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls the scale of a chemical
 */
public class ChemicalScale : ChemicalScalingPropertyBase<Vector3>
{
    protected override Vector3 GetCurrentValue()
    {
        return transform.localScale;
    }

    protected override Vector3 GetTargetValue(ChemicalData i_data)
    {
        return new Vector3(i_data.Scale, i_data.Scale, 1f);
    }

    protected override Vector3 LerpValue(Vector3 i_startValue, Vector3 i_targetValue, float i_lerp)
    {
        return Vector3.Lerp(i_startValue, i_targetValue, i_lerp);
    }

    protected override void SetValue(Vector3 i_newValue)
    {
        transform.localScale = i_newValue;
    }
}
