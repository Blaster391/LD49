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

    public string Name => m_name;
    public float Mass { get { return m_mass; } }
    public float Scale { get { return m_scale; } }
    public Color Color { get { return m_color; } }

    [System.Serializable]
    public struct TemperatureReaction
    {
        public float m_upperBound;
        public ChemicalData m_result;
    }
    public List<TemperatureReaction> m_temperatureReactions = new List<TemperatureReaction>();
}
