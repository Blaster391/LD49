using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundButtonScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text = null;

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetInt("Sound") == 0)
        {
            m_text.text = $"Sound: OFF";
        }
        else
        {
            m_text.text = $"Sound: ON";
        }
    }
}
