using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPrioritizationManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string envVolLabel;
    public List<AudioSource> characterAudioSourceList;
    public bool lowerEnvOnSpeechSetting = true;


    // Start is called before the first frame update
    void Start()
    {
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
                    audioMixer.SetFloat(envVolLabel, -30);
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
                    audioMixer.SetFloat(envVolLabel, 0);
                    break;
                }
            }
        }
    }

    public void lowerEnvSoundsVolume(AudioSource audioSource)
    {
        if (lowerEnvOnSpeechSetting)
        {
            audioMixer.SetFloat(envVolLabel, -30);
            StartCoroutine(recoverEnvSoundsVolume(audioSource.clip.length));
        }
    }

    IEnumerator recoverEnvSoundsVolume(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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
                audioMixer.SetFloat(envVolLabel, 0);
                break;
            }
        }
    }
}
