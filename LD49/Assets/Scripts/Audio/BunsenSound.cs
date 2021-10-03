using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_bunsenGeneralSound;
    [SerializeField]
    private AudioClip m_bunsenStartSound;
    [SerializeField]
    private AudioClip m_bunsenEndSound;

    [SerializeField]
    private float m_volumeMod = 1.0f;

    private AudioSource m_activeSound = null;
    private SoundManager m_manager = null;
    private IBunsenControl m_bunsen = null;

    // Start is called before the first frame update
    void Start()
    {
        m_manager = GetComponentInParent<SoundManager>();
        m_bunsen = GetComponent<IBunsenControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bunsen.Power > 0.05f)
        {
            if (m_activeSound == null)
            {
                m_activeSound = m_manager.PlaySound(m_bunsenGeneralSound, true);
                m_manager.PlaySound(m_bunsenStartSound, true);
            }

            m_activeSound.loop = true;
            m_activeSound.volume = m_bunsen.Power * m_volumeMod;

        }
        else
        {
            if (m_activeSound != null)
            {
                m_activeSound.Stop();
                m_manager.PlaySound(m_bunsenEndSound, true);
            }
        }
    }
}
