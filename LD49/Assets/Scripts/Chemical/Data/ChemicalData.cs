using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChemicalData", menuName = "ScriptableObjects/ChemicalData", order = 2)]
public class ChemicalData : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] private float m_mass;
    [SerializeField] private float m_scale;
    [SerializeField] private Color m_color;
    [SerializeField] private MatterState m_matterState;

    public string Name => m_name;
    public float Mass { get { return m_mass; } }
    public float Scale { get { return m_scale; } }
    public Color Color { get { return m_color; } }
    public MatterState MatterState { get { return m_matterState; } }

    [System.Serializable]
    public struct TemperatureReaction
    {
        public float m_lowerBound;
        public float m_upperBound;
        public ChemicalData m_result;
    }
    [SerializeField] private List<TemperatureReaction> m_temperatureReactions = new List<TemperatureReaction>();
    public List<TemperatureReaction> TemperatureReactions => new List<TemperatureReaction>(m_temperatureReactions);
}
