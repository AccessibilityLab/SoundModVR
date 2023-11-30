using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveListenHelperManager : MonoBehaviour
{
    public AudioSource[] audioSoruceList;
    public float cutoff = 0.5f;
    private AudioListener _cameraAudioListener;
    private AudioListener _liveListenHelperListener;
    // Start is called before the first frame update
    void Start()
    {
        audioSoruceList = (AudioSource[])FindObjectsOfType(typeof(AudioSource));
        _cameraAudioListener = FindAnyObjectByType<Camera>().gameObject.GetComponent<AudioListener>();
        _liveListenHelperListener = GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_liveListenHelperListener.enabled) {
            float minDist = Mathf.Infinity;
            AudioSource closestAudioSource = null;
            foreach (AudioSource audioSource in audioSoruceList) {
                float dist = Vector3.Distance(audioSource.transform.position, transform.position);
                if (dist < minDist) { 
                    minDist = dist;
                    closestAudioSource = audioSource;
                }
            }
            if (closestAudioSource != null) {
                if (minDist <= cutoff)
                {
                    foreach (AudioSource audioSource in audioSoruceList)
                    {
                        audioSource.mute = true;
                    }
                    closestAudioSource.mute = false;
                }
                else {
                    foreach (AudioSource audioSource in audioSoruceList)
                    {
                        audioSource.mute = false;
                    }
                }
            }
        }
    }

    public void StartUsingLiveListenHelper() {
        _cameraAudioListener.enabled = false;
        _liveListenHelperListener.enabled = true;
    }

    public void StopUsingLiveListenHelper()
    {
        _cameraAudioListener.enabled = true;
        _liveListenHelperListener.enabled = false;
        foreach (AudioSource audioSource in audioSoruceList) {
            audioSource.mute = false;
        }
    }
}
