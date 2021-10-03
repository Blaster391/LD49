using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCollideSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_clip;

    [SerializeField]
    private float m_minPitch = 1.0f;
    [SerializeField]
    private float m_maxPitch = 1.0f;

    [SerializeField]
    private float m_minVolume = 1.0f;

    [SerializeField]
    private float m_maxVolume = 1.0f;

    private SoundManager m_soundManager = null;

    void Start()
    {
        m_soundManager = GetComponentInParent<SoundManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.position.x > transform.position.x && collision.gameObject.GetComponent<GlassCollideSound>() != null)
        {
            if(Time.timeSinceLevelLoad < 1)
            {
                return;
            }

            AudioSource source = m_soundManager.PlaySound(m_clip, false);

            if(source != null)
            {
                source.pitch = Random.Range(m_minPitch, m_maxPitch);
                source.volume = PlayerPrefs.GetFloat("Volume") * Random.Range(m_minVolume, m_maxVolume);
            }
        }
    }
}
