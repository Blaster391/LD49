using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenFlame : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer = null;
    [SerializeField] private Color m_onFireColor;

    private Color m_coolColor;

    private IBunsenControl m_bunsenControlIF = null;

    #region Unity
    void Awake()
    {
        m_bunsenControlIF = GetComponentInParent<IBunsenControl>();
    }

    void Start()
    {
        m_coolColor = m_spriteRenderer.color;
    }

    void Update()
    {
        m_spriteRenderer.color = Color.Lerp(m_coolColor, m_onFireColor, m_bunsenControlIF.Power);
    }
    #endregion
}
