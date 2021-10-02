using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls the scale of a chemical
 */
public abstract class ChemicalScalingPropertyBase<TProperty> : MonoBehaviour
{
    [SerializeField] private float m_scaleTime = 1f;

    private IChemicalState m_chemicalStateIF = null;

    private Coroutine m_scaleRoutine = null;

    #region Unity
    void Awake()
    {
        m_chemicalStateIF = GetComponent<IChemicalState>();
    }

    void Start()
    {
        m_chemicalStateIF.StateChanged += OnStateChange;

        SetValue(GetTargetValue(m_chemicalStateIF.State));
    }
    #endregion

    #region Listeners
    private void OnStateChange(ChemicalData i_from, ChemicalData i_to)
    {
        if(m_scaleRoutine != null)
        {
            StopCoroutine(m_scaleRoutine);
        }
        m_scaleRoutine = StartCoroutine(ChangeScale(GetTargetValue(i_to)));
    }
    #endregion

    #region Routines
    private IEnumerator ChangeScale(TProperty i_targetValue)
    {
        TProperty oldValue = GetCurrentValue();
        float startTime = Time.time;

        while(Time.time < startTime + m_scaleTime)
        {
            SetValue(LerpValue(oldValue, i_targetValue, (Time.time - startTime) / m_scaleTime));
            yield return null;
        }

        SetValue(i_targetValue);
    }
    #endregion

    #region Abstract
    protected abstract TProperty GetCurrentValue();
    protected abstract TProperty GetTargetValue(ChemicalData i_data);
    protected abstract TProperty LerpValue(TProperty i_startValue, TProperty i_targetValue, float i_lerp);
    protected abstract void SetValue(TProperty i_newValue);
    #endregion
}
