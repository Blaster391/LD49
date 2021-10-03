using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_bgmSource = null;
    [SerializeField]
    private List<AudioSource> m_sources = new List<AudioSource>();

    private bool m_muted = false;
 
    void Update()
    {
        bool muted = PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetInt("Sound") == 0;
        if(muted != m_muted)
        {
            m_muted = muted;
            m_bgmSource.mute = m_muted;
            foreach(var source in m_sources)
            {
                source.mute = m_muted;
            }
        }

    }
}
