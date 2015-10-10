using UnityEngine;
using System.Collections;

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

    public void MuteButtonPress()
    {
        m_IsMuted = !m_IsMuted;
        if (m_IsMuted)
        {
            m_AudioSpawner.ChangeVolume(0.0f);
        }
        else
        {
            m_AudioSpawner.ChangeVolume(1.0f);
        }
    }
}
