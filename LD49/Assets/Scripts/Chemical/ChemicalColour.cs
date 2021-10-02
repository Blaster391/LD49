using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls the colour of a chemical
 */
[RequireComponent(typeof(SpriteRenderer))]
public class ChemicalColour : ChemicalScalingPropertyBase<Color>
{
    [SerializeField] private SpriteRenderer m_spriteRenderer = null;

    protected override Color GetCurrentValue()
    {
        return m_spriteRenderer.color;
    }

    protected override Color GetTargetValue(ChemicalData i_data)
    {
        return i_data.Color;
    }

    protected override Color LerpValue(Color i_startValue, Color i_targetValue, float i_lerp)
    {
        return Color.Lerp(i_startValue, i_targetValue, i_lerp);
    }

    protected override void SetValue(Color i_newValue)
    {
        m_spriteRenderer.color = i_newValue;
    }
}
