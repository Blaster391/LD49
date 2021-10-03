using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookState : MonoBehaviour
{
    [SerializeField] private Button m_openCloseButton = null;
    [SerializeField] private AnimationCurve m_openingCurve;

    private RectTransform m_rectTransform = null;

    // State
    private bool m_open = false;
    private float m_openProp = 0f;

    #region Unity
    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        m_openCloseButton.onClick.AddListener(OnOpenCloseClicked);
    }

    void Update()
    {
        float closedX = m_rectTransform.rect.width;

        // Tend toward the position
        m_openProp = Mathf.Clamp01(m_openProp + (m_open ? Time.deltaTime : -Time.deltaTime));

        float positionProp = m_openingCurve.Evaluate(m_openProp);

        Vector3 position = m_rectTransform.anchoredPosition;
        position.x = (1 - positionProp) * closedX;
        m_rectTransform.anchoredPosition = position;
    }
    #endregion

    #region Behavior

    #endregion

    #region Listeners
    private void OnOpenCloseClicked()
    {
        m_open = !m_open;
    }
    #endregion
}
