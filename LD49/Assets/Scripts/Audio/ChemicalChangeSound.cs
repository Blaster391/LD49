using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalChangeSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_collision;
    [SerializeField]
    private AudioClip m_heat;
    [SerializeField]
    private AudioClip m_cool;

    private SoundManager m_soundManager = null;

    void Start()
    {
        m_soundManager = GetComponentInParent<SoundManager>();
    }

    public void CollisionSound()
    {
        m_soundManager.PlaySound(m_collision, false);
    }

    public void HeatSound()
    {
        m_soundManager.PlaySound(m_heat, false);
    }

    public void CoolSound()
    {
        m_soundManager.PlaySound(m_cool, false);
    }
}
