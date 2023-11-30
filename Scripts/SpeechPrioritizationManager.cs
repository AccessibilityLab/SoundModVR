using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SpeechPrioritizationManager : MonoBehaviour
{
    public AudioMixer Environment_Mixer;
    public string envMixerVolLabel;
    public List<AudioSource> characterAudioSourceList;
    
    [System.NonSerialized]
    public bool lowerEnvOnSpeechSetting = false;
    public IEnumerator recoverEnvSoundsVolumeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        recoverEnvSoundsVolumeCoroutine = recoverEnvSoundsVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLowerEnvOnSpeechSetting(bool toggle) {
        lowerEnvOnSpeechSetting = toggle;
        if (lowerEnvOnSpeechSetting )
        {
            foreach (AudioSource audioSource in characterAudioSourceList) {
                if (audioSource.isPlaying) {
                    Environment_Mixer.SetFloat(envMixerVolLabel, -30);
                    break;
                }
            }
        }
        if (!lowerEnvOnSpeechSetting)
        {
            foreach (AudioSource audioSource in characterAudioSourceList)
            {
                if (audioSource.isPlaying)
                {
                    Environment_Mixer.SetFloat(envMixerVolLabel, 0);
                    break;
                }
            }
        }
    }

    public void lowerEnvSoundsVolume()
    {
        Environment_Mixer.SetFloat(envMixerVolLabel, -30);
    }

    IEnumerator recoverEnvSoundsVolume()
    {
        bool stopped = true;
        while (true)
        {
            stopped = true;
            foreach (AudioSource audioSource in characterAudioSourceList)
            {
                if (audioSource.isPlaying)
                {
                    stopped = false;
                    yield return new WaitForSeconds(1);
                    break;
                }
            }
            if (stopped)
            {
                Environment_Mixer.SetFloat(envMixerVolLabel, 0);
                break;
            }
        }
    }
}
