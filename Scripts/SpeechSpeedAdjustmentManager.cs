using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpeechSpeedAdjustmentManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioMixer _MasterMixer;
    public string audioMixerPitchLabel;
    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShiftSpeed(float speedValue)
    {
        if (audioSource != null && audioMixerPitchLabel != null)
        {
            audioSource.pitch = speedValue;
            _MasterMixer.SetFloat(audioMixerPitchLabel, 1 / speedValue);
        }
    }
}
