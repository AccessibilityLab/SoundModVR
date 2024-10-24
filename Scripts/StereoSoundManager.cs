using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StereoSoundManager : MonoBehaviour
{
    private float stereoPanVal = 0;
    private float spatialBlend = 1;
    // public Slider leftRightBalanceSlider;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAllSoundSourceWithStereo(float v) {
        // Get All AudioSource
        // set it with value
        // set public value stereoPanVal to v
        
        foreach (AudioSource audioSource in audioManager.staticAudioSourceList) {
            audioSource.panStereo = v;
        }
        foreach (AudioSource audioSource in audioManager.nonStaticAudioSourceList)
        {
            audioSource.panStereo = v;
        }
        stereoPanVal = v;
    }

    public void SetAllSoundSource2D_3D(bool bool_2D) {
        // get All AudioSource
        // if is2D, chnage everything to 2D
        // if !is2D, change everything except Background Music to 3D
        // set public value is2D to b
        foreach (AudioSource audioSource in audioManager.nonStaticAudioSourceList)
        {
            if (bool_2D)
            {
                audioSource.spatialBlend = 0;
                spatialBlend = 0;
            }
            else {
                audioSource.spatialBlend = 1;
                spatialBlend = 1;
            }
        }
    }

    public void SetSourceWithSetting(AudioSource audioSource) { 
        // used when spawning a new AudioSource
        // will set the value on that new audioSOurce to the one stored.
        audioSource.panStereo = stereoPanVal;
        audioSource.spatialBlend = spatialBlend;
    }

    // public void ResetLeftRightBalance() {
    //     leftRightBalanceSlider.value = 0f;
    // }
}
