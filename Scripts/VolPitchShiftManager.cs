using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolPitchShiftManager : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup;
    public AudioSource[] audioSourcesList;
    public string pitchLabel;
    public string volumeLabel;
    // Start is called before the first frame update
    void Start()
    {
        if (audioSourcesList == null || audioSourcesList.Length == 0)
        {
            // we assume user want to apply the shifts to all sound sources
            AudioSource[] allAudioSources = (AudioSource[])FindObjectsOfType(typeof(AudioSource));
            foreach (AudioSource audioSource in allAudioSources)
            {
                AssignAudioMixer(audioSource);
            }
        }
        else {
            foreach (AudioSource audioSource in audioSourcesList)
            {
                AssignAudioMixer(audioSource);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftPitch(float val)
    {
        if (audioMixerGroup != null && audioMixerGroup.audioMixer != null)
        {
            audioMixerGroup.audioMixer.SetFloat(pitchLabel, val);
        }
    }

    public void ShiftVolume(float val)
    {
        if (audioMixerGroup != null && audioMixerGroup.audioMixer != null)
        {
            audioMixerGroup.audioMixer.SetFloat(volumeLabel, val);
        }
    }

    public void AssignAudioMixer(AudioSource audioSource) {
        audioSource.outputAudioMixerGroup = audioMixerGroup;
    }
}
