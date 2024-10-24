using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class DirectionPrioritizationManager : MonoBehaviour
{
    public AudioMixerGroup prioritizationAudioMixer;
    public AudioMixerGroup generalAudioMixer;
    public string generalAudioVolumeLabel;
    public Camera mainCamera;
    public float degreeThreshold = 10f;
    private AudioSource focusAudioSource;
    private AudioMixerGroup focusAudioOrigAudioMixer;
    private AudioManager audioManager;
    private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn) {
            if (focusAudioSource)
            {
                focusAudioSource.outputAudioMixerGroup = focusAudioOrigAudioMixer;
                focusAudioOrigAudioMixer = null;
                focusAudioSource = null;
                generalAudioMixer.audioMixer.SetFloat(generalAudioVolumeLabel, 0f);
            }
            return;
        }
        if (mainCamera == null || prioritizationAudioMixer == null)
        {
            return;
        }
        if (IsCloseAngleToCamera(focusAudioSource))
        {
            return;
        } else {
            if (focusAudioSource)
            {
                focusAudioSource.outputAudioMixerGroup = focusAudioOrigAudioMixer;
                focusAudioOrigAudioMixer = null;
                focusAudioSource = null;
                generalAudioMixer.audioMixer.SetFloat(generalAudioVolumeLabel, 0f);
            }
        }
        foreach (AudioSource audioSource in audioManager.nonStaticAudioSourceList) {
            if (IsCloseAngleToCamera(audioSource)) {
                focusAudioOrigAudioMixer = audioSource.outputAudioMixerGroup;
                audioSource.outputAudioMixerGroup = prioritizationAudioMixer;
                focusAudioSource = audioSource;
                generalAudioMixer.audioMixer.SetFloat(generalAudioVolumeLabel, -20f);
                return;
            }
        }
    }


    bool IsCloseAngleToCamera(AudioSource audioSource)
    {
        if (audioSource == null) {
            return false;
        }
        if (Vector3.Distance(mainCamera.transform.position, audioSource.transform.position) > audioSource.maxDistance) {
            return false;
        }
        Vector3 cameraDirection = mainCamera.transform.forward;
        Vector3 targetDirection = audioSource.transform.position - mainCamera.transform.position;
        float angle = Vector3.Angle(targetDirection, cameraDirection);
        if (angle < degreeThreshold)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public void ToggleOnOff(bool toggle) {
        isOn = toggle;
    }
}
