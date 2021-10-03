using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_bgmSource = null;

    [SerializeField]
    private AudioSource m_sourcePrefab;

    [SerializeField]
    private int m_maxSounds = 10;

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

        var sourceCopy = m_sources.ToList();

        m_sources = m_sources.Where(x => x.isPlaying).ToList();

        foreach(var source in sourceCopy)
        {
            if(!source.isPlaying)
            {
                Destroy(source.gameObject);
            }
        }
    }


    public AudioSource PlaySound(AudioClip _clip, bool _force)
    {
        if(_clip == null)
        {
            Debug.LogError("NOT AN AUDIO");
            return null;
        }

        if (_force || m_sources.Count < m_maxSounds)
        {
            AudioSource newSound = Instantiate<AudioSource>(m_sourcePrefab, transform);
            newSound.mute = m_muted;
            newSound.clip = _clip;
            newSound.Play();
            m_sources.Add(newSound);
        }

        return null;
    }

    public void StopBGM()
    {
        m_bgmSource.Stop();
    }
}
