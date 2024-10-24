using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Audio Manager is in charge of keeping track of all the audioSource on scene.
    //And what audiosource is supposed to be statiic (eg: background, shoulder localization)
    public List<AudioSource> staticAudioSourceList;
    public List<AudioSource> nonStaticAudioSourceList;

    // Start is called before the first frame update
    void Start()
    {
        // At the start, collect all AudioSource in the scene.
        // if the audioSource is 2D, then it's static. If Spatial Blend is > 0, then it's non-static
        staticAudioSourceList = new List<AudioSource>();
        nonStaticAudioSourceList = new List<AudioSource>();
        AudioSource[] allAudioSources = (AudioSource[])FindObjectsOfType(typeof(AudioSource));
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.spatialBlend == 0)
            {
                staticAudioSourceList.Add(audioSource);
            }
            else { 
                nonStaticAudioSourceList.Add(audioSource);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EstablishNewAudioSource(AudioSource audioSource) {
        if (audioSource.spatialBlend == 0)
        {
            staticAudioSourceList.Add(audioSource);
        }
        else
        {
            nonStaticAudioSourceList.Add(audioSource);
        }
        StereoSoundManager stereoSoundManager = FindObjectOfType<StereoSoundManager>();
        if (stereoSoundManager != null)
        {
            stereoSoundManager.SetSourceWithSetting(audioSource);
        }
        SystemFreqVolShiftManager systemFreqVolShiftManager = FindObjectOfType<SystemFreqVolShiftManager>();
        if (systemFreqVolShiftManager != null)
        {
            systemFreqVolShiftManager.AssignAudioMixer(audioSource);
        }
    }
}
