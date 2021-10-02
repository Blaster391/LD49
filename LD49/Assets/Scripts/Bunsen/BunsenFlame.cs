using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenFlame : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer = null;
    [SerializeField] private Transform m_scaledTransform = null;

    [System.Serializable]
    private struct State
    {
        public Color m_color;
        public float m_scale;
    }

    [SerializeField] private State m_lowTempState;
    [SerializeField] private State m_highTempState;

    private IBunsenControl m_bunsenControlIF = null;

    #region Unity
    void Awake()
    {
        m_bunsenControlIF = GetComponentInParent<IBunsenControl>();
    }

    void Update()
    {
        m_spriteRenderer.color = Color.Lerp(m_lowTempState.m_color, m_highTempState.m_color, m_bunsenControlIF.Power);

        float scale = Mathf.Lerp(m_lowTempState.m_scale, m_highTempState.m_scale, m_bunsenControlIF.Power);
        m_scaledTransform.localScale = new Vector2(scale, scale);
    }
    #endregion
}
