using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTemperature : TemperatureComponent
{
    [SerializeField] [Range(0, 1)] private float m_heatTransferMult = 0.1f;
    [SerializeField] private SpriteRenderer m_spriteRenderer = null;
    [SerializeField] private Color m_coolColor = Color.cyan;
    [SerializeField] private Color m_hotColor = Color.red;

    private Container m_container = null;

    #region Unity
    private void Awake()
    {
        m_container = GetComponent<Container>();
    }

    protected override void Update()
    {
        base.Update();

        foreach (Chemical chemical in m_container.Contents.ContainedParticles)
        {
            chemical.Temperature.AlterTemperature(Temperature * m_heatTransferMult * Time.deltaTime);
        }

        if (Temperature > 0f)
        {
            m_spriteRenderer.color = Color.Lerp(Color.white, m_hotColor, HeatProp);
        }
        else
        {
            m_spriteRenderer.color = Color.Lerp(Color.white, m_coolColor, CoolProp);
        }
    }
    #endregion
}
