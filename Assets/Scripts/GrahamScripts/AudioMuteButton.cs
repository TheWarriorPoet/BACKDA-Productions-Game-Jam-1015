using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 10/10/2015
// Brief:  Switches the state of the Audio Spawner, whether its muted or not.
//      MuteButtonPress is intended to be called by a event trigger or 
//      onclick function in the UI
//////////////////////////////////////////////////////////////////////

public class AudioMuteButton : MonoBehaviour {

    public AudioSpawner m_AudioSpawner;

    private bool m_IsMuted = false;

    public List<AudioSource> m_AudioSources = new List<AudioSource>();

    void Start()
    {
        float m_AudioVolume = PlayerPrefs.GetFloat("AudioVolume", 1.0f);
        if (m_AudioSpawner != null) { m_AudioSpawner.ChangeVolume(m_AudioVolume); }
        foreach (AudioSource a in m_AudioSources)
        {
            a.volume = m_AudioVolume;
        }
        if (m_AudioVolume < 0.1f)
        {
            m_IsMuted = true;
        }
    }

    public void MuteButtonPress()
    {
        m_IsMuted = !m_IsMuted;
        if (m_IsMuted)
        {
            if (m_AudioSpawner != null) { m_AudioSpawner.ChangeVolume(0.0f); }
            foreach (AudioSource a in m_AudioSources)
            {
                a.volume = 0.0f;
                PlayerPrefs.SetFloat("AudioVolume", 0.0f);
            }
        }
        else
        {
            if (m_AudioSpawner != null) { m_AudioSpawner.ChangeVolume(1.0f); }
            foreach (AudioSource a in m_AudioSources)
            {
                a.volume = 1.0f;
                PlayerPrefs.SetFloat("AudioVolume", 1.0f);
            }
        }
    }
}
