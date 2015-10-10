using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//////////////////////////////////////////////////////////////////////
// Author:  Graham Johnston
// Date Created: 09/10/2015
// Brief: Spawns Audio tracks on a function call, also controls their lifetime
//      management, storage and retrieval using a dictionary
//////////////////////////////////////////////////////////////////////

public class AudioSpawner : MonoBehaviour {

    // Dictionary because we dont need random access just quick searching for deletion
    // Slow Add/Remove times shouldnt be an issue unless we create many of these per frame
    // which is unlikely considering the project
    Dictionary<string, GameObject> m_AudioMap = new Dictionary<string, GameObject>();

    public GameObject m_DefaultPosititon;

    // Storage of any volume changes.
    private float m_Volume = 1.0f;

    // Provide a percentage between 0.0 and 1.0, sets the audio volume of every object it has in the list.
    public void ChangeVolume(float a_Percentage)
    {
        a_Percentage = Mathf.Clamp(a_Percentage, 0.0f, 1.0f);

        // This is not something we really should be doing for a dictionary
        // We do need a way to set volume though, and this seems to be the way to do it.
        foreach(var g in m_AudioMap)
        {
            g.Value.GetComponent<AudioSource>().volume = a_Percentage;
        }
        m_Volume = a_Percentage;
    }

    // Finds the Audio Source stored at that key and returns it. Will return null if none found.
    public AudioSource RetrieveAudioByUniqueName(string a_UniqueName)
    {
        GameObject audioGameObject;
        if(m_AudioMap.TryGetValue(a_UniqueName,out audioGameObject)) // Attempts to find the value in the dictionary
        {
            // If found, return the attached AudioSource
            return audioGameObject.GetComponent<AudioSource>();
        }
        else { Debug.Log("audio object not found"); return null; } // Otherwise return null
    }

    // Retrieves and destroys the gameobject related to the given audiosource, 
    //then removes that object from the list, will return null if not found.
    public void DeleteAudioObjectByName(string a_UniqueName)
    {
        GameObject audioGameObject;
        if (m_AudioMap.TryGetValue(a_UniqueName, out audioGameObject)) // Attempts to find the value in the dictionary
        {
            Destroy(audioGameObject);
            m_AudioMap.Remove(a_UniqueName);// Removes the object from the map.
        }
        else { Debug.Log("audio object not found"); }
    }

    // Spawns an audio source at the default location using the clip provided, storing it in a map with
    // the uniqueName Provided
    public AudioSource SpawnAudio(string a_UniqueName, AudioClip a_AudioTrack, bool a_PlayNow)
    {
        // Creates a new audio source at the position of m_DefaultPosition gameobject.
        GameObject newObject = new GameObject();
        newObject.transform.position = m_DefaultPosititon.transform.position;
        AudioSource attachedAudioSource = newObject.AddComponent<AudioSource>();
        attachedAudioSource.clip = a_AudioTrack;
        attachedAudioSource.volume = m_Volume;
        if (a_PlayNow)
        {
            attachedAudioSource.playOnAwake = true; // Should play in the next frame
        }
        else
        {
            attachedAudioSource.playOnAwake = false; // Should stop if from playing until called
        }
        m_AudioMap.Add(a_UniqueName, newObject);
        return attachedAudioSource;
    }
    // Spawns an audio source at the location of the gameobject using the clip provided, storing it in a map with
    // the uniqueName Provided
    public AudioSource SpawnAudio(string a_UniqueName, AudioClip a_AudioTrack, bool a_PlayNow, GameObject a_Position)
    {
        // Creates a new audio source at the position of m_DefaultPosition gameobject.
        GameObject newObject = new GameObject();
        newObject.transform.position = a_Position.transform.position;
        AudioSource attachedAudioSource = newObject.AddComponent<AudioSource>();
        attachedAudioSource.clip = a_AudioTrack;
        attachedAudioSource.volume = m_Volume;
        if (a_PlayNow)
        {
            attachedAudioSource.playOnAwake = true; // Should play in the next frame
        }
        else
        {
            attachedAudioSource.playOnAwake = false; // Should stop if from playing until called
        }
        m_AudioMap.Add(a_UniqueName, newObject);
        return attachedAudioSource;
    }
    // Spawns an audio source at the location of the gameobject using the clip provided, storing it in a map with
    // the uniqueName Provided. Sets the Parent Transform of the audio source in the heirarchy to the one provided
    public AudioSource SpawnAudio(string a_UniqueName, AudioClip a_AudioTrack, bool a_PlayNow, GameObject a_Position, Transform a_ParentTransform)
    {
        // Creates a new audio source at the position of m_DefaultPosition gameobject.
        GameObject newObject = new GameObject();
        newObject.transform.position = a_Position.transform.position;
        newObject.transform.SetParent(a_ParentTransform, true);
        AudioSource attachedAudioSource = newObject.AddComponent<AudioSource>();
        attachedAudioSource.clip = a_AudioTrack;
        attachedAudioSource.volume = m_Volume;
        if (a_PlayNow)
        {
            attachedAudioSource.playOnAwake = true; // Should play in the next frame
        }
        else
        {
            attachedAudioSource.playOnAwake = false; // Should stop if from playing until called
        }
        m_AudioMap.Add(a_UniqueName, newObject);
        return attachedAudioSource;
    }
    // Spawns an audio source at the location provided using the clip provided, storing it in a map with
    // the uniqueName Provided. Sets the Parent Transform of the audio source in the heirarchy to the one provided
    public AudioSource SpawnAudio(string a_UniqueName, AudioClip a_AudioTrack, bool a_PlayNow, Vector3 a_Position, Transform a_ParentTransform)
    {
        // Creates a new audio source at the position of m_DefaultPosition gameobject.
        GameObject newObject = new GameObject();
        newObject.transform.position = a_Position;
        newObject.transform.SetParent(a_ParentTransform, true);
        AudioSource attachedAudioSource = newObject.AddComponent<AudioSource>();
        attachedAudioSource.clip = a_AudioTrack;
        attachedAudioSource.volume = m_Volume;
        if (a_PlayNow)
        {
            attachedAudioSource.playOnAwake = true; // Should play in the next frame
        }
        else
        {
            attachedAudioSource.playOnAwake = false; // Should stop if from playing until called
        }
        m_AudioMap.Add(a_UniqueName, newObject);
        return attachedAudioSource;
    }

}
