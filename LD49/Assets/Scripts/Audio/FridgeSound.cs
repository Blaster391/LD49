using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_clip;

    [SerializeField]
    private float m_volume = 1.0f;

    private void Start()
    {
        AudioSource source = GetComponentInParent<SoundManager>().PlaySound(m_clip, true);
        source.volume = m_volume;
        source.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
